using System;

namespace Celeste.Mod.PrideMod.UI {
    public class OuiPrideSettings : AbstractSubmenu {

        public OuiPrideSettings()
            : base("modoptions_PrideMod_PrideSettings_title", "modoptions_PrideMod_PrideSettings") { }

        protected override void addOptionsToMenu(TextMenu menu, bool inGame, object[] parameters) {
            PrideModModuleSettings settings = PrideModModule.Settings;


            TextMenu.SubHeader header_CrystalHearts = CreateSubHeader("modoptions_PrideMod_header_CrystalHearts");
            TextMenu.Item item_ASideCrystalHeart = CreatePrideSettingSlider(
                "modoptions_PrideMod_ASideCrystalHeart",
                settings.ASideCrystalHeart,
                prideType => settings.ASideCrystalHeart = prideType
            );
            TextMenu.Item item_BSideCrystalHeart = CreatePrideSettingSlider(
                "modoptions_PrideMod_BSideCrystalHeart",
                settings.BSideCrystalHeart,
                prideType => settings.BSideCrystalHeart = prideType
            );
            TextMenu.Item item_CSideCrystalHeart = CreatePrideSettingSlider(
                "modoptions_PrideMod_CSideCrystalHeart",
                settings.CSideCrystalHeart,
                prideType => settings.CSideCrystalHeart = prideType
            );
            TextMenu.Item item_EmptyCrystalHeart = CreatePrideSettingSlider(
                "modoptions_PrideMod_EmptyCrystalHeart",
                settings.EmptyCrystalHeart,
                prideType => settings.EmptyCrystalHeart = prideType
            );
            TextMenu.Item item_GhostCrystalHeart = CreatePrideSettingSlider(
                "modoptions_PrideMod_GhostCrystalHeart",
                settings.GhostCrystalHeart,
                prideType => settings.GhostCrystalHeart = prideType
            );


            TextMenu.SubHeader header_Strawberries = CreateSubHeader("modoptions_PrideMod_header_Strawberries");
            TextMenu.Item item_Strawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_Strawberry",
                settings.Strawberry,
                prideType => settings.Strawberry = prideType
            );
            TextMenu.Item item_GhostStrawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_GhostStrawberry",
                settings.GhostStrawberry,
                prideType => settings.GhostStrawberry = prideType
            );
            TextMenu.Item item_GoldenStrawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_GoldenStrawberry",
                settings.GoldenStrawberry,
                prideType => settings.GoldenStrawberry = prideType
            );
            TextMenu.Item item_GhostGoldenStrawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_GhostGoldenStrawberry",
                settings.GhostGoldenStrawberry,
                prideType => settings.GhostGoldenStrawberry = prideType
            );
            TextMenu.Item item_SilverStrawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_SilverStrawberry",
                settings.SilverStrawberry,
                prideType => settings.SilverStrawberry = prideType
            );
            TextMenu.Item item_GhostSilverStrawberry = CreatePrideSettingSlider(
                "modoptions_PrideMod_GhostSilverStrawberry",
                settings.GhostSilverStrawberry,
                prideType => settings.GhostSilverStrawberry = prideType
            );


            TextMenu.SubHeader header_Flags = CreateSubHeader("modoptions_PrideMod_header_Flags");
            TextMenu.Item item_SummitFlag = CreatePrideSettingSlider(
                "modoptions_PrideMod_SummitFlag",
                settings.SummitFlag,
                prideType => settings.SummitFlag = prideType
            );
            TextMenu.Item item_FinalFlag = CreatePrideSettingSlider(
                "modoptions_PrideMod_FinalFlag",
                settings.FinalFlag,
                prideType => settings.FinalFlag = prideType
            );


            menu.Add(header_CrystalHearts);
            menu.Add(item_ASideCrystalHeart);
            menu.Add(item_BSideCrystalHeart);
            menu.Add(item_CSideCrystalHeart);
            menu.Add(item_EmptyCrystalHeart);
            menu.Add(item_GhostCrystalHeart);

            menu.Add(header_Strawberries);
            menu.Add(item_Strawberry);
            menu.Add(item_GhostStrawberry);
            menu.Add(item_GoldenStrawberry);
            menu.Add(item_GhostGoldenStrawberry);
            if (PrideModModule.Instance.Loaded_CollabUtils2) {
                menu.Add(item_SilverStrawberry);
                menu.Add(item_GhostSilverStrawberry);
            }

            menu.Add(header_Flags);
            menu.Add(item_SummitFlag);
            menu.Add(item_FinalFlag);


            item_ASideCrystalHeart      .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_ASideCrystalHeart_sub"));
            item_BSideCrystalHeart      .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_BSideCrystalHeart_sub"));
            item_CSideCrystalHeart      .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_CSideCrystalHeart_sub"));
            item_EmptyCrystalHeart      .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_EmptyCrystalHeart_sub"));
            item_GhostCrystalHeart      .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_GhostCrystalHeart_sub"));
            item_Strawberry             .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_Strawberry_sub"));
            item_GhostStrawberry        .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_GhostStrawberry_sub"));
            item_GoldenStrawberry       .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_GoldenStrawberry_sub"));
            item_GhostGoldenStrawberry  .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_GhostGoldenStrawberry_sub"));
            item_SilverStrawberry       .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_SilverStrawberry_sub"));
            item_GhostSilverStrawberry  .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_GhostSilverStrawberry_sub"));
            item_SummitFlag             .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_SummitFlag_sub"));
            item_FinalFlag              .AddDescription(menu, Dialog.Clean("modoptions_PrideMod_FinalFlag_sub"));
        }

        private static TextMenu.Item CreatePrideSettingSlider(string settingName, PrideTypes value, Action<PrideTypes> action)
            => new TextMenu.Slider(
                Dialog.Clean(settingName),
                i => ((PrideTypes)i).ToString(),
                0, PrideData.PrideCount - 1,
                (int)value
            ).Change(i => action((PrideTypes)i));

        private static TextMenu.SubHeader CreateSubHeader(string header)
            => new(Dialog.Clean(header));

        protected override void gotoMenu(Overworld overworld)
            => overworld.Goto<OuiPrideSettings>();
    }
}