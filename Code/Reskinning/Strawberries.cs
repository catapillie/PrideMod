using Mono.Cecil.Cil;
using Monocle;
using MonoMod.Cil;
using System;

namespace Celeste.Mod.PrideMod {
    public static class Strawberries {
        private const string ghostgoldberry_sprite                  = "goldghostberry";
        private const string ghostberry_sprite                      = "ghostberry";
        private const string goldberry_sprite                       = "goldberry";
        private const string strawberry_sprite                      = "strawberry";
        private const string collabutils2_ghostsilverberry_sprite   = "CollabUtils2_ghostSilverBerry";
        private const string collabutils2_silverberry_sprite        = "CollabUtils2_silverBerry";

        internal static void Hook() {
            IL.Celeste.Strawberry.Added += Mod_Strawberry_Added;
            On.Monocle.SpriteBank.Create += Mod_SpriteBank_Create;
        }

        internal static void Unhook() {
            IL.Celeste.Strawberry.Added -= Mod_Strawberry_Added;
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

            cursor.EmitDelegate<Func<float, float, float>>((alpha, _) => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                return settings.Enabled && settings.MinimalBloom ? 0.05f : alpha;
            });
            cursor.Emit(OpCodes.Ldc_R4, 12f);
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
