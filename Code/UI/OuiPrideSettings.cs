using System;

namespace Celeste.Mod.PrideMod.UI {
    public class OuiPrideSettings : AbstractSubmenu {
        public OuiPrideSettings()
            : base("modoptions_PrideMod_PrideSettings_title", "modoptions_PrideMod_PrideSettings") { }

        protected override void addOptionsToMenu(TextMenu menu, bool inGame, object[] parameters) {
            PrideModModuleSettings settings = PrideModModule.Settings;
            bool collabUtils2Loaded = PrideModModule.Instance.Loaded_CollabUtils2;


            CreateSubHeader     (menu, "modoptions_PrideMod_header_CrystalHearts");
            CreatePrideSetting  (menu, "modoptions_PrideMod_ASideCrystalHeart", settings.ASideCrystalHeart, prideType => settings.ASideCrystalHeart = prideType, displayDesc: true);
            CreatePrideSetting  (menu, "modoptions_PrideMod_BSideCrystalHeart", settings.BSideCrystalHeart, prideType => settings.BSideCrystalHeart = prideType);
            CreatePrideSetting  (menu, "modoptions_PrideMod_CSideCrystalHeart", settings.CSideCrystalHeart, prideType => settings.CSideCrystalHeart = prideType);
            CreatePrideSetting  (menu, "modoptions_PrideMod_EmptyCrystalHeart", settings.EmptyCrystalHeart, prideType => settings.EmptyCrystalHeart = prideType);
            CreatePrideSetting  (menu, "modoptions_PrideMod_GhostCrystalHeart", settings.GhostCrystalHeart, prideType => settings.GhostCrystalHeart = prideType);

            if (collabUtils2Loaded) {
                CreateSubHeader     (menu, "modoptions_PrideMod_header_MiniHearts");
                CreatePrideSetting  (menu, "modoptions_PrideMod_BeginnerMiniHeart", settings.BeginnerMiniHeart, prideType => settings.BeginnerMiniHeart = prideType);
                CreatePrideSetting  (menu, "modoptions_PrideMod_IntermediateMiniHeart", settings.IntermediateMiniHeart, prideType => settings.IntermediateMiniHeart = prideType);
                CreatePrideSetting  (menu, "modoptions_PrideMod_AdvancedMiniHeart", settings.AdvancedMiniHeart, prideType => settings.AdvancedMiniHeart = prideType);
                CreatePrideSetting  (menu, "modoptions_PrideMod_ExpertMiniHeart", settings.ExpertMiniHeart, prideType => settings.ExpertMiniHeart = prideType);
                CreatePrideSetting  (menu, "modoptions_PrideMod_GrandmasterMiniHeart", settings.GrandmasterMiniHeart, prideType => settings.GrandmasterMiniHeart = prideType);
                CreatePrideSetting  (menu, "modoptions_PrideMod_GhostMiniHeart", settings.GhostMiniHeart, prideType => settings.GhostMiniHeart = prideType);
            }

            CreateSubHeader     (menu, "modoptions_PrideMod_header_Strawberries");
            CreatePrideSetting  (menu, "modoptions_PrideMod_Strawberry", settings.Strawberry, prideType => settings.Strawberry = prideType);
            CreatePrideSetting  (menu, "modoptions_PrideMod_GhostStrawberry", settings.GhostStrawberry, prideType => settings.GhostStrawberry = prideType);
            CreatePrideSetting  (menu, "modoptions_PrideMod_GoldenStrawberry", settings.GoldenStrawberry, prideType => settings.GoldenStrawberry = prideType);
            CreatePrideSetting  (menu, "modoptions_PrideMod_GhostGoldenStrawberry", settings.GhostGoldenStrawberry, prideType => settings.GhostGoldenStrawberry = prideType);
            if (collabUtils2Loaded) {
                CreatePrideSetting(menu, "modoptions_PrideMod_SilverStrawberry", settings.SilverStrawberry, prideType => settings.SilverStrawberry = prideType);
                CreatePrideSetting(menu, "modoptions_PrideMod_GhostSilverStrawberry", settings.GhostSilverStrawberry, prideType => settings.GhostSilverStrawberry = prideType);
            }

            CreateSubHeader     (menu, "modoptions_PrideMod_header_Flags");
            CreatePrideSetting  (menu, "modoptions_PrideMod_SummitFlag", settings.SummitFlag, prideType => settings.SummitFlag = prideType);
            CreatePrideSetting  (menu, "modoptions_PrideMod_FinalFlag", settings.FinalFlag, prideType => settings.FinalFlag = prideType);
            CreatePrideSetting  (menu, "modoptions_PrideMod_Confetti", settings.Confetti, prideType => settings.Confetti = prideType);
        }

        private void CreatePrideSetting(TextMenu menu, string settingName, PrideTypes value, Action<PrideTypes> action, bool displayDesc = false) {
            TextMenu.Item item = new TextMenu.Slider(
                Dialog.Clean(settingName),
                i => ((PrideTypes)i).GetFormattedName(),
                0, PrideData.PrideCount - 1,
                (int)value
            ).Change(i => action((PrideTypes)i));

            menu.Add(item);
            item.AddDescription(menu, Dialog.Clean(settingName + "_sub"));

            // The first TextMenu.Item in the submenu would not display its description until reselected
            // so we want to show it anyway
            if (displayDesc)
                item.OnEnter();
        }

        private static void CreateSubHeader(TextMenu menu, string header)
            => menu.Add(new TextMenu.SubHeader(Dialog.Clean(header)));

        protected override void gotoMenu(Overworld overworld)
            => overworld.Goto<OuiPrideSettings>();
    }
}