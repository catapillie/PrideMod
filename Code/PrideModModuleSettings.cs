using Celeste.Mod.PrideMod.UI;
using Celeste.Mod.UI;
using YamlDotNet.Serialization;

namespace Celeste.Mod.PrideMod {
    [SettingName("modoptions_PrideMod")]
    public class PrideModModuleSettings : EverestModuleSettings {
        public bool Enabled { get; set; } = true;

        [SettingName("modoptions_PrideMod_MinimalBloom")]
        [SettingSubText("modoptions_PrideMod_MinimalBloom_sub")]
        public bool MinimalBloom { get; set; } = true;


        [SettingName("modoptions_PrideMod_ASideCrystalHeart")]
        [SettingSubText("modoptions_PrideMod_ASideCrystalHeart_sub")]
        [SettingIgnore]
        public PrideTypes ASideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMod_BSideCrystalHeart")]
        [SettingSubText("modoptions_PrideMod_BSideCrystalHeart_sub")]
        [SettingIgnore]
        public PrideTypes BSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMod_CSideCrystalHeart")]
        [SettingSubText("modoptions_PrideMod_CSideCrystalHeart_sub")]
        [SettingIgnore]
        public PrideTypes CSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMod_EmptyCrystalHeart")]
        [SettingSubText("modoptions_PrideMod_EmptyCrystalHeart_sub")]
        [SettingIgnore]
        public PrideTypes EmptyCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMod_GhostCrystalHeart")]
        [SettingSubText("modoptions_PrideMod_GhostCrystalHeart_sub")]
        [SettingIgnore]
        public PrideTypes GhostCrystalHeart { get; set; } = PrideTypes.Default;


        [SettingName("modoptions_PrideMod_Strawberry")]
        [SettingSubText("modoptions_PrideMod_Strawberry_sub")]
        [SettingIgnore]
        public PrideTypes Strawberry { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMod_GhostStrawberry")]
        [SettingSubText("modoptions_PrideMod_GhostStrawberry_sub")]
        [SettingIgnore]
        public PrideTypes GhostStrawberry { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMod_GoldenStrawberry")]
        [SettingSubText("modoptions_PrideMod_GoldenStrawberry_sub")]
        [SettingIgnore]
        public PrideTypes GoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMod_GhostGoldenStrawberry")]
        [SettingSubText("modoptions_PrideMod_GhostGoldenStrawberry_sub")]
        [SettingIgnore]
        public PrideTypes GhostGoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMod_SilverStrawberry")]
        [SettingSubText("modoptions_PrideMod_SilverStrawberry_sub")]
        [SettingIgnore]
        public PrideTypes SilverStrawberry { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMod_GhostSilverStrawberry")]
        [SettingSubText("modoptions_PrideMod_GhostSilverStrawberry_sub")]
        [SettingIgnore]
        public PrideTypes GhostSilverStrawberry { get; set; } = PrideTypes.Default;


        [SettingName("modoptions_PrideMod_SummitFlag")]
        [SettingSubText("modoptions_PrideMod_SummitFlag_sub")]
        [SettingIgnore]
        public PrideTypes SummitFlag { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMod_FinalFlag")]
        [SettingSubText("modoptions_PrideMod_FinalFlag_sub")]
        [SettingIgnore]
        public PrideTypes FinalFlag { get; set; } = PrideTypes.Default;


        [YamlIgnore]
        public int PrideSettings { get; set; } = 0;
        public void CreatePrideSettingsEntry(TextMenu menu, bool inGame) {
            menu.Add(
                AbstractSubmenu.BuildOpenMenuButton<OuiPrideSettings>(
                    menu,
                    inGame,
                    () => OuiModOptions.Instance.Overworld.Goto<OuiModOptions>(),
                    new object[0]
                )
            ); // :>
        }
    }
}
