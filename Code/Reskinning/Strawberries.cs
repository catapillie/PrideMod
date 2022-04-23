using Mono.Cecil.Cil;
using Monocle;
using MonoMod.Cil;
using MonoMod.Utils;
using System;

namespace Celeste.Mod.PrideMod.Reskinning {
    public static class Strawberries {
        private const string ghostgoldberry_sprite                  = "goldghostberry";
        private const string ghostberry_sprite                      = "ghostberry";
        private const string goldberry_sprite                       = "goldberry";
        private const string strawberry_sprite                      = "strawberry";
        private const string collabutils2_ghostsilverberry_sprite   = "CollabUtils2_ghostSilverBerry";
        private const string collabutils2_silverberry_sprite        = "CollabUtils2_silverBerry";

        private const string strawberryseed_sprite                  = "strawberrySeed";
        private const string goldberryseed_sprite                   = "goldberrySeed";
        private const string ghostberryseed                         = "ghostberrySeed";

        private const string CollabUtils2_Entities_SilverBerry_type = "Celeste.Mod.CollabUtils2.Entities.SilverBerry";

        internal static void Hook() {
            IL.Celeste.Strawberry.Added += Mod_Strawberry_Added;
            IL.Celeste.StrawberrySeed.Awake += Mod_StrawberrySeed_Awake;
            On.Monocle.SpriteBank.Create += Mod_SpriteBank_Create;
        }

        internal static void Unhook() {
            IL.Celeste.Strawberry.Added -= Mod_Strawberry_Added;
            IL.Celeste.StrawberrySeed.Awake -= Mod_StrawberrySeed_Awake;
            On.Monocle.SpriteBank.Create -= Mod_SpriteBank_Create;
        }

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
                        bool isSilverBerry = strawberry.GetType().FullName == CollabUtils2_Entities_SilverBerry_type;

                        if (isGhostBerry) {
                            applyMinimalBloom = (isSilverBerry ? settings.GhostSilverStrawberry : settings.GhostGoldenStrawberry) != PrideTypes.Default;
                        } else {
                            applyMinimalBloom = (isSilverBerry ? settings.SilverStrawberry : settings.GoldenStrawberry) != PrideTypes.Default;
                        }
                    } else {
                        if (isGhostBerry) {
                            applyMinimalBloom = settings.GhostStrawberry != PrideTypes.Default;
                        } else {
                            applyMinimalBloom = settings.Strawberry != PrideTypes.Default;
                        }
                    }

                    if (applyMinimalBloom)
                        alpha = 0.05f;
                }
                return alpha;
            });
            cursor.Emit(OpCodes.Ldc_R4, 12f);
        }

        private static void Mod_StrawberrySeed_Awake(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(strawberryseed_sprite));
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate<Func<string, StrawberrySeed, string>>((id, seed) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                if (settings.Enabled) {
                    id = settings.Strawberry.GetCustomSpriteID("strawberryseed", id);
                    if (settings.Strawberry != PrideTypes.Default)
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
                    if (settings.GoldenStrawberry != PrideTypes.Default)
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
                    if (settings.GhostStrawberry != PrideTypes.Default)
                        new DynData<StrawberrySeed>(seed).Get<BloomPoint>("bloom").Alpha = 0.05f;
                }
                return id;
            });
        }

        private static Sprite Mod_SpriteBank_Create(On.Monocle.SpriteBank.orig_Create orig, SpriteBank self, string id) {
            PrideModModuleSettings settings = PrideModModule.Settings;
            if (settings.Enabled) {
                id = id switch {
                    collabutils2_ghostsilverberry_sprite => settings.GhostSilverStrawberry.GetCustomSpriteID("ghostsilverberry", id),
                    collabutils2_silverberry_sprite => settings.SilverStrawberry.GetCustomSpriteID("silverberry", id),
                    _ => id,
                };
            }

            return orig(self, id);
        }
    }
}
