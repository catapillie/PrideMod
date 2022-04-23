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
                        id = settings.GhostCrystalHeart.GetHeartSpriteID(id);
                    else if (heartGem.IsFake)
                        id = settings.EmptyCrystalHeart.GetHeartSpriteID(id);
                    else {
                        Level level = heartGem.SceneAs<Level>();
                        id = level.Session.Area.Mode switch {
                            AreaMode.Normal => settings.ASideCrystalHeart.GetHeartSpriteID(id),
                            AreaMode.BSide => settings.BSideCrystalHeart.GetHeartSpriteID(id),
                            AreaMode.CSide => settings.CSideCrystalHeart.GetHeartSpriteID(id),
                            _ => id,
                        };
                    }
                }

                return id;
            });
        }
    }
}
