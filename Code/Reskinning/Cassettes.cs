using MonoMod.Cil;
using System;

namespace Celeste.Mod.PrideMod.Reskinning {
    public static class Cassettes {
        private const string cassette_sprite        = "cassette";
        private const string cassetteghost_sprite   = "cassetteGhost";

        internal static void Hook() {
            IL.Celeste.Cassette.Added += Mod_Cassette_Added;
        }

        internal static void Unhook() {
            IL.Celeste.Cassette.Added += Mod_Cassette_Added;
        }

        private static void Mod_Cassette_Added(ILContext il) {
            ILCursor cursor = new(il);

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(cassette_sprite));
            cursor.EmitDelegate<Func<string, string>>(id => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                return settings.Enabled ? settings.Cassette.GetCustomSpriteID("cassette", id) : id;
            });

            cursor.GotoNext(MoveType.After, instr => instr.MatchLdstr(cassetteghost_sprite));
            cursor.EmitDelegate<Func<string, string>>(id => {
                PrideModModuleSettings settings = PrideModModule.Settings;
                return settings.Enabled ? settings.GhostCassette.GetCustomSpriteID("cassette", id) : id;
            });
        }
    }
}
