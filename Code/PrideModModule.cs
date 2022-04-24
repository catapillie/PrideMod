using System;

namespace Celeste.Mod.PrideMod {
    public class PrideModModule : EverestModule {
        public static PrideModModule Instance { get; private set; }

        public override Type SettingsType => typeof(PrideModModuleSettings);
        public static PrideModModuleSettings Settings => (PrideModModuleSettings)Instance._Settings;

        public bool Loaded_CollabUtils2 { get; private set; }

        public PrideModModule() {
            Instance = this;
        }

        public override void Load() {
            Reskinning.CrystalHearts.Hook();
            Reskinning.MiniHearts.Hook();
            Reskinning.Strawberries.Hook();
            Reskinning.FlagDecals.Hook();

            FindOptionalDependencies();
        }

        public override void Unload() {
            Reskinning.CrystalHearts.Unhook();
            Reskinning.MiniHearts.Unhook();
            Reskinning.Strawberries.Unhook();
            Reskinning.FlagDecals.Unhook();
        }

        private void FindOptionalDependencies() {
            Loaded_CollabUtils2 = Everest.Loader.DependencyLoaded(new() {
                Name = "CollabUtils2",
                VersionString = "1.6.10",
            });
        }
    }
}