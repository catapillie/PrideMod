﻿using Celeste.Mod.CollabUtils2.Entities;
using Celeste.Mod.PrideMod.Components;
using Mono.Cecil.Cil;
using Monocle;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using System;
using System.Reflection;

namespace Celeste.Mod.PrideMod.Reskinning {
    public static class MiniHearts {
        private const string beginnerminiheart_sprite       = "CollabUtils2/miniheart/beginner/";
        private const string intermediateminiheart_sprite   = "CollabUtils2/miniheart/intermediate/";
        private const string advancedminiheart_sprite       = "CollabUtils2/miniheart/advanced/";
        private const string expertminiheart_sprite         = "CollabUtils2/miniheart/expert/";
        private const string grandmasterminiheart_sprite    = "CollabUtils2/miniheart/grandmaster/";
        private const string ghostminiheart_sprite          = "CollabUtils2/miniheart/ghost/ghost";

        private const string PrideMod_AbstractMiniHeart_wasReskinned = "PrideMod_AbstractMiniHeart_wasReskinned";

        private static ILHook IL_AbstractMiniHeart_Added;

        internal static void Hook() { }

        internal static void Unhook() { }

        internal static void Hook_CollabUtils2() {
            MethodInfo m_AbstractMiniHeart_Added
                = typeof(AbstractMiniHeart).GetMethod("Added", BindingFlags.Instance | BindingFlags.Public);

            IL_AbstractMiniHeart_Added = new ILHook(m_AbstractMiniHeart_Added, Mod_AbstractMiniHeart_Added);
        }

        internal static void Unhook_CollabUtils2() {
            IL_AbstractMiniHeart_Added.Dispose();
        }

        private static void Mod_AbstractMiniHeart_Added(ILContext il) {
            MethodInfo m_Image_CenterOrigin
                = typeof(Image).GetMethod("CenterOrigin", BindingFlags.Instance | BindingFlags.Public);

            ILCursor cursor = new(il);

            cursor.GotoNext(instr => instr.MatchCallvirt(m_Image_CenterOrigin));
            cursor.GotoNext(MoveType.After, instr => instr.MatchPop());

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Action<AbstractMiniHeart>>(miniHeart => {
                PrideModModuleSettings settings = PrideModModule.Settings;

                if (settings.Enabled) {
                    DynData<AbstractMiniHeart> data = new(miniHeart);

                    Pride pride = Pride.Default;

                    Sprite sprite = (Sprite)data["sprite"];
                    Sprite newSprite = sprite.Path switch {
                        beginnerminiheart_sprite => (pride = settings.BeginnerMiniHeart).GetCustomSprite("miniheart", sprite),
                        intermediateminiheart_sprite => (pride = settings.IntermediateMiniHeart).GetCustomSprite("miniheart", sprite),
                        advancedminiheart_sprite => (pride = settings.AdvancedMiniHeart).GetCustomSprite("miniheart", sprite),
                        expertminiheart_sprite => (pride = settings.ExpertMiniHeart).GetCustomSprite("miniheart", sprite),
                        grandmasterminiheart_sprite => (pride = settings.GrandmasterMiniHeart).GetCustomSprite("miniheart", sprite),
                        ghostminiheart_sprite => (pride = settings.GhostMiniHeart).GetCustomSprite("miniheart", sprite),
                        _ => sprite,
                    };

                    if (sprite != newSprite) {
                        miniHeart.Remove(sprite);
                        miniHeart.Add(newSprite);
                        data["sprite"] = newSprite;
                        data["PrideMod_AbstractMiniHeart_wasReskinned"] = true;
                        miniHeart.Add(new MiniHeartParticleChanger(data, PrideData.PrideParticles_HeartGem_P_AnyShine[pride]));
                    }
                }
            });

            cursor.GotoNext(instr => instr.MatchNewobj<BloomPoint>());
            cursor.GotoPrev(MoveType.After, instr => instr.MatchLdcR4(0.75f));

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<float, AbstractMiniHeart, float>>((alpha, miniHeart) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled && settings.MinimalBloom) {
                    DynData<AbstractMiniHeart> data = new(miniHeart);
                    if (data.Data.TryGetValue(PrideMod_AbstractMiniHeart_wasReskinned, out var value) && (bool)value)
                        alpha = 0.05f;
                }
                return alpha;
            });
        }
    }
}
