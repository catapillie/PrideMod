using System;

namespace Celeste.Mod.PrideMod.UI {
    public class OuiPrideSettings : AbstractSubmenu {
        private TextMenu.Item
            Item_ASideCrystalHeart,
            Item_BSideCrystalHeart,
            Item_CSideCrystalHeart,
            Item_EmptyCrystalHeart,
            Item_GhostCrystalHeart,
            Item_Strawberry,
            Item_GhostStrawberry,
            Item_GoldenStrawberry,
            Item_GhostGoldenStrawberry,
            Item_SilverStrawberry,
            Item_GhostSilverStrawberry,
            Item_SummitFlag,
            Item_FinalFlag;


        public OuiPrideSettings()
            : base("modoptions_PrideMod_PrideSettings_title", "modoptions_PrideMod_PrideSettings") { }

        protected override void addOptionsToMenu(TextMenu menu, bool inGame, object[] parameters) {
            PrideModModuleSettings settings = PrideModModule.Settings;

            menu.Add(Item_ASideCrystalHeart = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_ASideCrystalHeart",
                settings.ASideCrystalHeart,
                prideType => settings.ASideCrystalHeart = prideType
            ));
            menu.Add(Item_BSideCrystalHeart = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_BSideCrystalHeart",
                settings.BSideCrystalHeart,
                prideType => settings.BSideCrystalHeart = prideType
            ));
            menu.Add(Item_CSideCrystalHeart = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_CSideCrystalHeart",
                settings.CSideCrystalHeart,
                prideType => settings.CSideCrystalHeart = prideType
            ));
            menu.Add(Item_EmptyCrystalHeart = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_EmptyCrystalHeart",
                settings.EmptyCrystalHeart,
                prideType => settings.EmptyCrystalHeart = prideType
            ));
            menu.Add(Item_GhostCrystalHeart = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_GhostCrystalHeart",
                settings.GhostCrystalHeart,
                prideType => settings.GhostCrystalHeart = prideType
            ));

            menu.Add(Item_Strawberry = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_Strawberry",
                settings.Strawberry,
                prideType => settings.Strawberry = prideType
            ));
            menu.Add(Item_GhostStrawberry = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_GhostStrawberry",
                settings.GhostStrawberry,
                prideType => settings.GhostStrawberry = prideType
            ));
            menu.Add(Item_GoldenStrawberry = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_GoldenStrawberry",
                settings.GoldenStrawberry,
                prideType => settings.GoldenStrawberry = prideType
            ));
            menu.Add(Item_GhostGoldenStrawberry = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_GhostGoldenStrawberry",
                settings.GhostGoldenStrawberry,
                prideType => settings.GhostGoldenStrawberry = prideType
            ));
            menu.Add(Item_SilverStrawberry = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_SilverStrawberry",
                settings.SilverStrawberry,
                prideType => settings.SilverStrawberry = prideType
            ));
            menu.Add(Item_GhostSilverStrawberry = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_GhostSilverStrawberry",
                settings.GhostSilverStrawberry,
                prideType => settings.GhostSilverStrawberry = prideType
            ));

            menu.Add(Item_SummitFlag = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_SummitFlag",
                settings.SummitFlag,
                prideType => settings.SummitFlag = prideType
            ));
            menu.Add(Item_FinalFlag = CreatePrideSettingSlider(
                menu,
                "modoptions_PrideMod_FinalFlag",
                settings.FinalFlag,
                prideType => settings.FinalFlag = prideType
            ));

            Item_ASideCrystalHeart      .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_ASideCrystalHeart_sub"));
            Item_BSideCrystalHeart      .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_BSideCrystalHeart_sub"));
            Item_CSideCrystalHeart      .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_CSideCrystalHeart_sub"));
            Item_EmptyCrystalHeart      .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_EmptyCrystalHeart_sub"));
            Item_GhostCrystalHeart      .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_GhostCrystalHeart_sub"));
            Item_Strawberry             .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_Strawberry_sub"));
            Item_GhostStrawberry        .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_GhostStrawberry_sub"));
            Item_GoldenStrawberry       .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_GoldenStrawberry_sub"));
            Item_GhostGoldenStrawberry  .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_GhostGoldenStrawberry_sub"));
            Item_SilverStrawberry       .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_SilverStrawberry_sub"));
            Item_GhostSilverStrawberry  .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_GhostSilverStrawberry_sub"));
            Item_SummitFlag             .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_SummitFlag_sub"));
            Item_FinalFlag              .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_FinalFlag_sub"));
        }


        private static TextMenu.Item CreatePrideSettingSlider(TextMenu menu, string settingName, PrideTypes value, Action<PrideTypes> action)
            => new TextMenu.Slider(
                Dialog.Clean(settingName),
                i => ((PrideTypes)i).ToString(),
                0, PrideTypesInfo.PrideCount - 1,
                (int)value
            ).Change(i => action((PrideTypes)i));

        protected override void gotoMenu(Overworld overworld)
            => overworld.Goto<OuiPrideSettings>();
    }
}