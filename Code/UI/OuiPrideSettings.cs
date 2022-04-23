using System;

namespace Celeste.Mod.PrideMod.UI {
    public class OuiPrideSettings : AbstractSubmenu {
        public OuiPrideSettings()
            : base("modoptions_PrideMod_PrideSettings_title", "modoptions_PrideMod_PrideSettings") { }

        protected override void addOptionsToMenu(TextMenu menu, bool inGame, object[] parameters) {
            PrideModModuleSettings settings = PrideModModule.Settings;

            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_ASideCrystalHeart",
                settings.ASideCrystalHeart,
                prideType => settings.ASideCrystalHeart = prideType
            ));
            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_BSideCrystalHeart",
                settings.BSideCrystalHeart,
                prideType => settings.BSideCrystalHeart = prideType
            ));
            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_CSideCrystalHeart",
                settings.CSideCrystalHeart,
                prideType => settings.CSideCrystalHeart = prideType
            ));
            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_EmptyCrystalHeart",
                settings.EmptyCrystalHeart,
                prideType => settings.EmptyCrystalHeart = prideType
            ));
            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_GhostCrystalHeart",
                settings.GhostCrystalHeart,
                prideType => settings.GhostCrystalHeart = prideType
            ));

            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_Strawberry",
                settings.Strawberry,
                prideType => settings.Strawberry = prideType
            ));
            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_GhostStrawberry",
                settings.GhostStrawberry,
                prideType => settings.GhostStrawberry = prideType
            ));
            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_GoldenStrawberry",
                settings.GoldenStrawberry,
                prideType => settings.GoldenStrawberry = prideType
            ));
            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_GhostGoldenStrawberry",
                settings.GhostGoldenStrawberry,
                prideType => settings.GhostGoldenStrawberry = prideType
            ));
            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_SilverStrawberry",
                settings.SilverStrawberry,
                prideType => settings.SilverStrawberry = prideType
            ));
            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_GhostSilverStrawberry",
                settings.GhostSilverStrawberry,
                prideType => settings.GhostSilverStrawberry = prideType
            ));

            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_SummitFlag",
                settings.SummitFlag,
                prideType => settings.SummitFlag = prideType
            ));
            menu.Add(CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_FinalFlag",
                settings.FinalFlag,
                prideType => settings.FinalFlag = prideType
            ));
        }


        private static TextMenu.Item CreatePrideSettingSlider(TextMenu menu, string settingName, PrideTypes value, Action<PrideTypes> action)
            => new TextMenu.Slider(
                Dialog.Clean(settingName),
                i => ((PrideTypes)i).ToString(),
                0, PrideTypesInfo.PrideCount - 1,
                (int)value
            ).Change(i => action((PrideTypes)i))
             .AddDescription(menu, settingName + "_sub");

        protected override void gotoMenu(Overworld overworld)
            => overworld.Goto<OuiPrideSettings>();
    }
}