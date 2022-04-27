using Celeste.Mod.CollabUtils2.Entities;
using Mono.Cecil.Cil;
using Monocle;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using System;
using System.Linq;
using System.Reflection;

namespace Celeste.Mod.PrideMod.Reskinning {
    public static class Strawberries {
        private const string silverberry_firstframe                 = "CollabUtils2/silverBerry/idle00";

        private const string ghostgoldberry_sprite                  = "goldghostberry";
        private const string ghostberry_sprite                      = "ghostberry";
        private const string goldberry_sprite                       = "goldberry";
        private const string strawberry_sprite                      = "strawberry";
        private const string collabutils2_ghostsilverberry_sprite   = "CollabUtils2_ghostSilverBerry";
        private const string collabutils2_silverberry_sprite        = "CollabUtils2_silverBerry";

        private const string strawberryseed_sprite                  = "strawberrySeed";
        private const string goldberryseed_sprite                   = "goldberrySeed";
        private const string ghostberryseed                         = "ghostberrySeed";

        private const string CollabUtils2_RainbowBerryUnlockCutscene_typename = "RainbowBerryUnlockCutscene";

        private static ILHook IL_RainbowBerryUnlockCutscene_Cutscene;

        internal static void Hook() {
            IL.Celeste.Strawberry.Added += Mod_Strawberry_Added;
            IL.Celeste.StrawberrySeed.Awake += Mod_StrawberrySeed_Awake;
        }

        internal static void Unhook() {
            IL.Celeste.Strawberry.Added -= Mod_Strawberry_Added;
            IL.Celeste.StrawberrySeed.Awake -= Mod_StrawberrySeed_Awake;
        }

        internal static void Hook_CollabUtils2() {
            Type t_CollabUtils2_RainbowBerryUnlockCutscene
                = Dependencies.CollabUtils2_Module
                    .GetType().Assembly
                    .GetTypesSafe()
                    .First(type => type.Name == CollabUtils2_RainbowBerryUnlockCutscene_typename);

            MethodInfo m_RainbowBerryUnlockCutscene_Cutscene
                = t_CollabUtils2_RainbowBerryUnlockCutscene.GetMethod("Cutscene", BindingFlags.Instance | BindingFlags.NonPublic).GetStateMachineTarget();

            IL_RainbowBerryUnlockCutscene_Cutscene = new ILHook(m_RainbowBerryUnlockCutscene_Cutscene, Mod_RainbowBerryUnlockCutscene_Cutscene);

            On.Celeste.Strawberry.Added += Mod_Strawberry_Added;
        }

        internal static void Unhook_CollabUtils2() {
            IL_RainbowBerryUnlockCutscene_Cutscene.Dispose();

            On.Celeste.Strawberry.Added -= Mod_Strawberry_Added;
        }

        private static bool CollabUtils2_Loaded_SilverBerryCheck(Strawberry strawberry)
            => strawberry is SilverBerry;

        private static void Mod_Strawberry_Added(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(ghostgoldberry_sprite));
            cursor.EmitDelegate<Func<string, string>>(id => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                return settings.Enabled ? settings.GhostGoldenStrawberry.GetCustomSpriteID("ghostgoldenberry", id) : id;
            });

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(ghostberry_sprite));
            cursor.EmitDelegate<Func<string, string>>(id => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                return settings.Enabled ? settings.GhostStrawberry.GetCustomSpriteID("ghostberry", id) : id;
            });

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(goldberry_sprite));
            cursor.EmitDelegate<Func<string, string>>(id => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                return settings.Enabled ? settings.GoldenStrawberry.GetCustomSpriteID("goldenberry", id) : id;
            });

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(strawberry_sprite));
            cursor.EmitDelegate<Func<string, string>>(id => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                return settings.Enabled ? settings.Strawberry.GetCustomSpriteID("strawberry", id) : id;
            });


            cursor.GotoNext(instr => instr.MatchNewobj<BloomPoint>());
            cursor.GotoPrev(MoveType.After, instr => instr.MatchLdcR4(12));

            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<float, float, Strawberry, float>>((alpha, _, strawberry) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled && settings.MinimalBloom) {
                    bool isGhostBerry = (bool) new DynData<Strawberry>(strawberry)["isGhostBerry"];

                    bool applyMinimalBloom = false;

                    if (strawberry.Golden) {
                        bool isSilverBerry = Dependencies.CollabUtils2_Loaded && CollabUtils2_Loaded_SilverBerryCheck(strawberry);

                        if (isGhostBerry) {
                            applyMinimalBloom = (isSilverBerry ? settings.GhostSilverStrawberry : settings.GhostGoldenStrawberry) != Pride.Default;
                        } else {
                            applyMinimalBloom = (isSilverBerry ? settings.SilverStrawberry : settings.GoldenStrawberry) != Pride.Default;
                        }
                    } else {
                        if (isGhostBerry) {
                            applyMinimalBloom = settings.GhostStrawberry != Pride.Default;
                        } else {
                            applyMinimalBloom = settings.Strawberry != Pride.Default;
                        }
                    }

                    if (applyMinimalBloom)
                        alpha = 0.05f;
                }
                return alpha;
            });
            cursor.Emit(OpCodes.Ldc_R4, 12f);
        }

        private static void Mod_Strawberry_Added(On.Celeste.Strawberry.orig_Added orig, Strawberry self, Scene scene) {
            orig(self, scene);

            PrideModModuleSettings settings = PrideModModule.Settings;
            if (settings.Enabled && self is SilverBerry) {
                DynData<Strawberry> data = new(self);

                bool isGhostBerry = (bool)data["isGhostBerry"];
                Pride pride = isGhostBerry ? settings.GhostSilverStrawberry : settings.SilverStrawberry;

                if (pride != Pride.Default) {

                    Sprite oldSprite = (Sprite)data["sprite"];
                    Sprite sprite = pride.GetCustomSprite(isGhostBerry ? "ghostsilverberry" : "silverberry", oldSprite);

                    self.Remove(oldSprite);
                    self.Add(sprite);

                    if (self.Winged)
                        sprite.Play("flap");
                    sprite.OnFrameChange = oldSprite.OnFrameChange;

                    data["sprite"] = sprite;
                }
            }
        }

        private static void Mod_StrawberrySeed_Awake(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(strawberryseed_sprite));
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<string, StrawberrySeed, string>>((id, seed) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled) {
                    id = settings.Strawberry.GetCustomSpriteID("strawberryseed", id);
                    if (settings.Strawberry != Pride.Default)
                        new DynData<StrawberrySeed>(seed).Get<BloomPoint>("bloom").Alpha = 0.05f;
                }
                return id;
            });

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(goldberryseed_sprite));
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<string, StrawberrySeed, string>>((id, seed) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled) {
                    id = settings.GoldenStrawberry.GetCustomSpriteID("goldenberryseed", id);
                    if (settings.GoldenStrawberry != Pride.Default)
                        new DynData<StrawberrySeed>(seed).Get<BloomPoint>("bloom").Alpha = 0.05f;
                }
                return id;
            });

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(ghostberryseed));
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<string, StrawberrySeed, string>>((id, seed) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled) {
                    id = settings.GhostStrawberry.GetCustomSpriteID("ghostberryseed", id);
                    if (settings.GhostStrawberry != Pride.Default)
                        new DynData<StrawberrySeed>(seed).Get<BloomPoint>("bloom").Alpha = 0.05f;
                }
                return id;
            });
        }

        private static void Mod_RainbowBerryUnlockCutscene_Cutscene(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(silverberry_firstframe));

            cursor.EmitDelegate<Func<string, string>>(image => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                return settings.Enabled ? settings.SilverStrawberry.GetCustomTexturePath("silverberry", "idle00", image) : image;
            });
        }
    }
}
