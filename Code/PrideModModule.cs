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

        }

        public override void LoadContent(bool firstLoad) {
            base.LoadContent(firstLoad);

            PrideModSpriteBanks.Load();
        }

        public override void Unload() {

        }
    }
}