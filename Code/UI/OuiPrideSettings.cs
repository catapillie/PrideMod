using System;

namespace Celeste.Mod.PrideMod.UI {
    public class OuiPrideSettings : AbstractSubmenu {

        public OuiPrideSettings()
            : base("modoptions_PrideMod_PrideSettings_title", "modoptions_PrideMod_PrideSettings") { }

        protected override void addOptionsToMenu(TextMenu menu, bool inGame, object[] parameters) {
            PrideModModuleSettings settings = PrideModModule.Settings;

            TextMenu.Item Item_ASideCrystalHeart = CreatePrideSettingSlider(
                "modoptions_PrideMod_ASideCrystalHeart",
                settings.ASideCrystalHeart,
                prideType => settings.ASideCrystalHeart = prideType
            );
            TextMenu.Item Item_BSideCrystalHeart = CreatePrideSettingSlider(
                "modoptions_PrideMod_BSideCrystalHeart",
                settings.BSideCrystalHeart,
                prideType => settings.BSideCrystalHeart = prideType
            );
            TextMenu.Item Item_CSideCrystalHeart = CreatePrideSettingSlider(
                "modoptions_PrideMod_CSideCrystalHeart",
                settings.CSideCrystalHeart,
                prideType => settings.CSideCrystalHeart = prideType
            );
            TextMenu.Item Item_EmptyCrystalHeart = CreatePrideSettingSlider(
                "modoptions_PrideMod_EmptyCrystalHeart",
                settings.EmptyCrystalHeart,
                prideType => settings.EmptyCrystalHeart = prideType
            );
            TextMenu.Item Item_GhostCrystalHeart = CreatePrideSettingSlider(
                "modoptions_PrideMod_GhostCrystalHeart",
                settings.GhostCrystalHeart,
                prideType => settings.GhostCrystalHeart = prideType
            );

            TextMenu.Item Item_Strawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_Strawberry",
                settings.Strawberry,
                prideType => settings.Strawberry = prideType
            );
            TextMenu.Item Item_GhostStrawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_GhostStrawberry",
                settings.GhostStrawberry,
                prideType => settings.GhostStrawberry = prideType
            );
            TextMenu.Item Item_GoldenStrawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_GoldenStrawberry",
                settings.GoldenStrawberry,
                prideType => settings.GoldenStrawberry = prideType
            );
            TextMenu.Item Item_GhostGoldenStrawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_GhostGoldenStrawberry",
                settings.GhostGoldenStrawberry,
                prideType => settings.GhostGoldenStrawberry = prideType
            );
            TextMenu.Item Item_SilverStrawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_SilverStrawberry",
                settings.SilverStrawberry,
                prideType => settings.SilverStrawberry = prideType
            );
            TextMenu.Item Item_GhostSilverStrawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_GhostSilverStrawberry",
                settings.GhostSilverStrawberry,
                prideType => settings.GhostSilverStrawberry = prideType
            );

            TextMenu.Item Item_SummitFlag = CreatePrideSettingSlider(
                "modoptions_PrideMod_SummitFlag",
                settings.SummitFlag,
                prideType => settings.SummitFlag = prideType
            );
            TextMenu.Item Item_FinalFlag = CreatePrideSettingSlider(
                "modoptions_PrideMod_FinalFlag",
                settings.FinalFlag,
                prideType => settings.FinalFlag = prideType
            );

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

            menu.Add(Item_ASideCrystalHeart);
            menu.Add(Item_BSideCrystalHeart);
            menu.Add(Item_CSideCrystalHeart);
            menu.Add(Item_EmptyCrystalHeart);
            menu.Add(Item_GhostCrystalHeart);
            menu.Add(Item_Strawberry);
            menu.Add(Item_GhostStrawberry);
            menu.Add(Item_GoldenStrawberry);
            menu.Add(Item_GhostGoldenStrawberry);
            if (PrideModModule.Instance.Loaded_CollabUtils2) {
                menu.Add(Item_SilverStrawberry);
                menu.Add(Item_GhostSilverStrawberry);
            }
            menu.Add(Item_SummitFlag);
            menu.Add(Item_FinalFlag);
        }


        private static TextMenu.Item CreatePrideSettingSlider(string settingName, PrideTypes value, Action<PrideTypes> action)
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