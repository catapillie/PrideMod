using Celeste.Mod.PrideMod.Components;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;

namespace Celeste.Mod.PrideMod.Reskinning {
    public static class CrystalHearts {
        private static readonly FieldInfo f_GFX_SpriteBank
            = typeof(GFX).GetField("SpriteBank", BindingFlags.Static | BindingFlags.Public);

        internal static void Hook() {
            IL.Celeste.HeartGem.Awake += Mod_HeartGem_Awake;
            On.Celeste.HeartGem.Collect += Mod_HeartGem_Collect;
        }

        internal static void Unhook() {
            IL.Celeste.HeartGem.Awake -= Mod_HeartGem_Awake;
            On.Celeste.HeartGem.Collect -= Mod_HeartGem_Collect;
        }

        private static void Mod_HeartGem_Awake(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(instr => instr.MatchLdsfld(f_GFX_SpriteBank));
            cursor.GotoNext(MoveType.After, instr => instr.MatchLdloc(1));

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<string, HeartGem, string>>((id, heartGem) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled) {
                    Pride pride;

                    if (heartGem.IsGhost)
                        pride = settings.GhostCrystalHeart;
                    else if (heartGem.IsFake)
                        pride = settings.EmptyCrystalHeart;
                    else {
                        Level level = heartGem.SceneAs<Level>();
                        pride = level.Session.Area.Mode switch {
                            AreaMode.Normal => settings.ASideCrystalHeart,
                            AreaMode.BSide => settings.BSideCrystalHeart,
                            AreaMode.CSide => settings.CSideCrystalHeart,
                            _ => Pride.Default,
                        };
                    }

                    string newID = pride.GetCustomSpriteID("crystalheart", id);

                    if (id != newID) {
                        id = newID;
                        heartGem.Add(new CrystalHeartParticleChanger(
                            new(heartGem),
                            (heartGem.IsFake ? PrideData.PrideParticles_HeartGem_P_FakeShine : PrideData.PrideParticles_HeartGem_P_AnyShine)[pride]
                        ));
                    }
                }

                return id;
            });


            cursor.GotoNext(instr => instr.MatchNewobj<BloomPoint>());
            cursor.GotoPrev(MoveType.After, instr => instr.MatchLdcR4(0.75f));

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<float, HeartGem, float>>((alpha, heartGem) => {
                PrideModModuleSettings settings = PrideModModule.Settings;

                if (settings.Enabled && settings.MinimalBloom) {
                    bool applyMinimalBloom = false;

                    if (heartGem.IsGhost)
                        applyMinimalBloom = settings.GhostCrystalHeart != Pride.Default;
                    else if (heartGem.IsFake)
                        applyMinimalBloom = settings.EmptyCrystalHeart != Pride.Default;
                    else {
                        Level level = heartGem.SceneAs<Level>();
                        applyMinimalBloom = level.Session.Area.Mode switch {
                            AreaMode.Normal => settings.ASideCrystalHeart != Pride.Default,
                            AreaMode.BSide => settings.BSideCrystalHeart != Pride.Default,
                            AreaMode.CSide => settings.CSideCrystalHeart != Pride.Default,
                            _ => false,
                        };
                    }

                    if (applyMinimalBloom)
                        alpha = 0.05f;
                }

                return alpha;
            });
        }

        private static void Mod_HeartGem_Collect(On.Celeste.HeartGem.orig_Collect orig, HeartGem self, Player player) {
            self.Get<CrystalHeartParticleChanger>()?.RemoveSelf();
            orig(self, player);
        }
    }
}
