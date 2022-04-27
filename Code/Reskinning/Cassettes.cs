using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.Utils;
using System;

namespace Celeste.Mod.PrideMod.Reskinning {
    public static class Cassettes {
        private const string cassette_sprite        = "cassette";
        private const string cassetteghost_sprite   = "cassetteGhost";

        private const string PrideMod_Cassette_wasReskinned = "PrideMod_Cassette_wasReskinned";

        internal static void Hook() {
            IL.Celeste.Cassette.Added += Mod_Cassette_Added;
        }

        internal static void Unhook() {
            IL.Celeste.Cassette.Added += Mod_Cassette_Added;
        }

        private static void Mod_Cassette_Added(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(cassette_sprite));

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<string, Cassette, string>>((id, cassette) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled) {
                    if (settings.Cassette != PrideTypes.Default) {
                        id = settings.Cassette.GetCustomSpriteID("cassette", id);
                        new DynData<Cassette>(cassette)[PrideMod_Cassette_wasReskinned] = true;
                    }
                }

                return id;
            });

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(cassetteghost_sprite));

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<string, Cassette, string>>((id, cassette) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled) {
                    if (settings.GhostCassette != PrideTypes.Default) {
                        id = settings.GhostCassette.GetCustomSpriteID("cassette", id);
                        new DynData<Cassette>(cassette)[PrideMod_Cassette_wasReskinned] = true;
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
                    if (data.Data.TryGetValue(PrideMod_Cassette_wasReskinned, out var value) && (bool)value)
                        alpha = 0.05f;
                }

                return alpha;
            });
        }
    }
}
