﻿using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;

namespace Celeste.Mod.PrideMod {
    public static class CrystalHearts {
        private static readonly FieldInfo f_GFX_SpriteBank
            = typeof(GFX).GetField("SpriteBank", BindingFlags.Static | BindingFlags.Public);

        internal static void Hook() {
            IL.Celeste.HeartGem.Awake += Mod_HeartGem_Awake;
        }

        internal static void Unhook() {
            IL.Celeste.HeartGem.Awake -= Mod_HeartGem_Awake;
        }

        private static void Mod_HeartGem_Awake(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(instr => instr.MatchLdsfld(f_GFX_SpriteBank));
            cursor.GotoNext(MoveType.After, instr => instr.MatchLdloc(1));

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<string, HeartGem, string>>((id, heartGem) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled) {
                    if (heartGem.IsGhost)
                        id = settings.GhostCrystalHeart.GetCustomSpriteID("crystalheart", id);
                    else if (heartGem.IsFake)
                        id = settings.EmptyCrystalHeart.GetCustomSpriteID("crystalheart", id);
                    else {
                        Level level = heartGem.SceneAs<Level>();
                        id = level.Session.Area.Mode switch {
                            AreaMode.Normal => settings.ASideCrystalHeart.GetCustomSpriteID("crystalheart", id),
                            AreaMode.BSide => settings.BSideCrystalHeart.GetCustomSpriteID("crystalheart", id),
                            AreaMode.CSide => settings.CSideCrystalHeart.GetCustomSpriteID("crystalheart", id),
                            _ => id,
                        };
                    }
                }

                return id;
            });


            cursor.GotoNext(instr => instr.MatchNewobj<BloomPoint>());
            cursor.GotoPrev(MoveType.After, instr => instr.MatchLdcR4(0.75f));

            cursor.EmitDelegate<Func<float, float>>(alpha => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                return settings.Enabled && settings.MinimalBloom ? 0.05f : alpha;
            });
        }
    }
}
