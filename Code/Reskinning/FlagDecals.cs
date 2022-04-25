using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using Monocle;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using System;
using System.Reflection;

namespace Celeste.Mod.PrideMod.Reskinning {
    public static class FlagDecals {
        private static readonly MethodInfo m_Decal_orig_ctor
            = typeof(Decal).GetMethod("orig_ctor", BindingFlags.Instance | BindingFlags.Public);

        private static readonly FieldInfo f_Decal_Name
            = typeof(Decal).GetField("Name", BindingFlags.Instance | BindingFlags.Public);

        private static readonly FieldInfo f_SummitCheckpoint_ConfettiRenderer_confettiColors
            = typeof(SummitCheckpoint.ConfettiRenderer).GetField("confettiColors", BindingFlags.Static | BindingFlags.NonPublic);

        private const string summitflag_decal   = "decals/7-summit/SummitFlag";
        private const string finalflag_decal    = "decals/10-farewell/finalflag";

        private static ILHook IL_Decal_orig_ctor;

        internal static void Hook() {
            IL_Decal_orig_ctor = new ILHook(m_Decal_orig_ctor, Mod_Decal_orig_ctor);
            IL.Celeste.SummitCheckpoint.ConfettiRenderer.ctor += Mod_ConfettiRenderer_ctor;
        }

        internal static void Unhook() {
            IL_Decal_orig_ctor.Dispose();
            IL.Celeste.SummitCheckpoint.ConfettiRenderer.ctor -= Mod_ConfettiRenderer_ctor;
        }

        private static void Mod_Decal_orig_ctor(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdfld(f_Decal_Name));
            cursor.EmitDelegate<Func<string, string>>(name => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled) {
                    name = name switch {
                        summitflag_decal => settings.SummitFlag.GetCustomSummitFlagDecalPath(name),
                        finalflag_decal => settings.FinalFlag.GetCustomFinalFlagDecalPath(name),
                        _ => name,
                    };
                }

                return name;
            });
        }

        private static void Mod_ConfettiRenderer_ctor(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdsfld(f_SummitCheckpoint_ConfettiRenderer_confettiColors));
            cursor.EmitDelegate<Func<Color[], Color[]>>(colors => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                return settings.Enabled && settings.Confetti != PrideTypes.Default ?
                    PrideData.PrideColors[settings.Confetti] :
                    colors;
            });
        }
    }
}
