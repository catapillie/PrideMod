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


        [SettingIgnore]
        public PrideTypes ASideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        public PrideTypes BSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        public PrideTypes CSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        public PrideTypes EmptyCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        public PrideTypes GhostCrystalHeart { get; set; } = PrideTypes.Default;


        [SettingIgnore]
        public PrideTypes Strawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        public PrideTypes GhostStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        public PrideTypes GoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        public PrideTypes GhostGoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        public PrideTypes SilverStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        public PrideTypes GhostSilverStrawberry { get; set; } = PrideTypes.Default;


        [SettingIgnore]
        public PrideTypes SummitFlag { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        public PrideTypes FinalFlag { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        public PrideTypes Confetti { get; set; } = PrideTypes.Default;


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
