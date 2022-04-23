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
            Reskinning.CrystalHearts.Hook();
            Reskinning.Strawberries.Hook();
            Reskinning.FlagDecals.Hook();
        }

        public override void Unload() {
            Reskinning.CrystalHearts.Unhook();
            Reskinning.Strawberries.Unhook();
            Reskinning.FlagDecals.Unhook();
        }
    }
}