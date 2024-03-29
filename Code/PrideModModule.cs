﻿using System;
using System.Linq;

namespace Celeste.Mod.PrideMod {
    public class PrideModModule : EverestModule {
        public static PrideModModule Instance { get; private set; }

        public override Type SettingsType => typeof(PrideModModuleSettings);
        public static PrideModModuleSettings Settings => (PrideModModuleSettings)Instance._Settings;

        public EverestModuleMetadata[] AllDependencies { get; private set; }

        public PrideModModule() {
            Instance = this;
        }

        public override void LoadContent(bool firstLoad) {
            base.LoadContent(firstLoad);

            PrideData.InitializeContent();

            UI.ConfettiPrideSlider.InitializeContent();
        }

        public override void Load() {
            AllDependencies = Enumerable.Concat(Metadata.Dependencies, Metadata.OptionalDependencies).ToArray();

            Reskinning.CrystalHearts.Hook();
            Reskinning.MiniHearts.Hook();
            Reskinning.Strawberries.Hook();
            Reskinning.FlagDecals.Hook();
            Reskinning.Cassettes.Hook();

            UI.PrideSliderBase.Hook();
            UI.GlobalPrideButton.Hook();

            Everest.Events.Everest.OnLoadMod += Dependencies.Everest_OnLoadMod;
            Dependencies.Hook();
        }

        public override void Unload() {
            Reskinning.CrystalHearts.Unhook();
            Reskinning.MiniHearts.Unhook();
            Reskinning.Strawberries.Unhook();
            Reskinning.FlagDecals.Unhook();
            Reskinning.Cassettes.Unhook();

            UI.PrideSliderBase.Unhook();
            UI.GlobalPrideButton.Unhook();

            Everest.Events.Everest.OnLoadMod -= Dependencies.Everest_OnLoadMod;
            Dependencies.Unhook();
        }

        internal static void Log(string msg)
            => Logger.Log("Pride Mod", msg);
    }
}