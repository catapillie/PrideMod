using Celeste.Mod.PrideMod.Components;
using Mono.Cecil.Cil;
using Monocle;
using MonoMod.Cil;
using MonoMod.Utils;
using System;
using System.Reflection;

namespace Celeste.Mod.PrideMod.Reskinning {
    public static class Cassettes {
        private static readonly FieldInfo f_Cassette_P_Shine
            = typeof(Cassette).GetField("P_Shine", BindingFlags.Static | BindingFlags.Public);

        private const string cassette_sprite        = "cassette";
        private const string cassetteghost_sprite   = "cassetteGhost";

        private const string PrideMod_Cassette_pride = "PrideMod_Cassette_pride";

        internal static void Hook() {
            IL.Celeste.Cassette.Added += Mod_Cassette_Added;
            IL.Celeste.Cassette.Update += Mod_Cassette_Update;
        }

        internal static void Unhook() {
            IL.Celeste.Cassette.Added -= Mod_Cassette_Added;
            IL.Celeste.Cassette.Update -= Mod_Cassette_Update;
        }

        private static void Mod_Cassette_Added(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(cassette_sprite));

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<string, Cassette, string>>((id, cassette) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled) {
                    if (settings.Cassette != Pride.Default) {
                        id = settings.Cassette.GetCustomSpriteID("cassette");
                        new DynData<Cassette>(cassette)[PrideMod_Cassette_pride] = settings.Cassette;
                    }
                }

                return id;
            });

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(cassetteghost_sprite));

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<string, Cassette, string>>((id, cassette) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled) {
                    if (settings.GhostCassette != Pride.Default) {
                        id = settings.GhostCassette.GetCustomSpriteID("cassette");
                        new DynData<Cassette>(cassette)[PrideMod_Cassette_pride] = settings.GhostCassette;
                    }
                }

                return id;
            });

            cursor.GotoNext(instr => instr.MatchNewobj<BloomPoint>());
            cursor.GotoPrev(MoveType.After, instr => instr.MatchLdcR4(0.25f));

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<float, Cassette, float>>((alpha, cassette) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled && settings.MinimalBloom) {
                    DynData<Cassette> data = new(cassette);

                    if (data.Data.TryGetValue(PrideMod_Cassette_pride, out var value)) {
                        Pride pride = (Pride)value;

                        if (pride != Pride.Default) {
                            alpha = 0.05f;
                            cassette.Add(new CassetteParticleChanger(PrideData.PrideParticles_Cassette_P_Shine[pride]));
                        }
                    }
                }

                return alpha;
            });
        }

        private static void Mod_Cassette_Update(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdsfld(f_Cassette_P_Shine));

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<ParticleType, Cassette, ParticleType>>((p, cassette) => {
                CassetteParticleChanger component = cassette.Get<CassetteParticleChanger>();
                if (component != null)
                    p = component.Particle;
                return p;
            });
        }
    }
}
