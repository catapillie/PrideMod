using System;

namespace Celeste.Mod.PrideMod {
    public class PrideModModule : EverestModule {
        public static PrideModModule Instance { get; private set; }

        public override Type SettingsType => typeof(PrideModModuleSettings);
        public static PrideModModuleSettings Settings => (PrideModModuleSettings) Instance._Settings;

        public PrideModModule() {
            Instance = this;
        }

        public override void Load() {
            CrystalHearts.Hook();
        }

        public override void Unload() {
            CrystalHearts.Unhook();
        }
    }
}