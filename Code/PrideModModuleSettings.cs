namespace Celeste.Mod.PrideMod {
    [SettingName("modoptions_PrideMod")]
    public class PrideModModuleSettings : EverestModuleSettings {
        public bool Enabled { get; set; } = true;

        [SettingName("modoptions_PrideMod_BloomModifier")]
        [SettingSubText("modoptions_PrideMod_BloomModifier_sub")]
        public BloomTypes BloomModifier { get; set; } = BloomTypes.Minimal;


        [SettingName("modoptions_PrideMode_ASideCrystalHeart")]
        [SettingSubText("modoptions_PrideMode_ASideCrystalHeart_sub")]
        public PrideTypes ASideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMode_BSideCrystalHeart")]
        [SettingSubText("modoptions_PrideMode_BSideCrystalHeart_sub")]
        public PrideTypes BSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMode_CSideCrystalHeart")]
        [SettingSubText("modoptions_PrideMode_CSideCrystalHeart_sub")]
        public PrideTypes CSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMode_EmptyCrystalHeart")]
        [SettingSubText("modoptions_PrideMode_EmptyCrystalHeart_sub")]
        public PrideTypes EmptyCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMode_GhostCrystalHeart")]
        [SettingSubText("modoptions_PrideMode_GhostCrystalHeart_sub")]
        public PrideTypes GhostCrystalHeart { get; set; } = PrideTypes.Default;


        [SettingName("modoptions_PrideMode_Strawberry")]
        [SettingSubText("modoptions_PrideMode_Strawberry_sub")]
        public PrideTypes Strawberry { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMode_GhostStrawberry")]
        [SettingSubText("modoptions_PrideMode_GhostStrawberry_sub")]
        public PrideTypes GhostStrawberry { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMode_GoldenStrawberry")]
        [SettingSubText("modoptions_PrideMode_GoldenStrawberry_sub")]
        public PrideTypes GoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMode_GhostGoldenStrawberry")]
        [SettingSubText("modoptions_PrideMode_GhostGoldenStrawberry_sub")]
        public PrideTypes GhostGoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMode_SilverStrawberry")]
        [SettingSubText("modoptions_PrideMode_SilverStrawberry_sub")]
        public PrideTypes SilverStrawberry { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMode_GhostSilverStrawberry")]
        [SettingSubText("modoptions_PrideMode_GhostSilverStrawberry_sub")]
        public PrideTypes GhostSilverStrawberry { get; set; } = PrideTypes.Default;


        [SettingName("modoptions_PrideMode_SummitFlag")]
        [SettingSubText("modoptions_PrideMode_SummitFlag_sub")]
        public PrideTypes SummitFlag { get; set; } = PrideTypes.Default;

        [SettingName("modoptions_PrideMode_FinalFlag")]
        [SettingSubText("modoptions_PrideMode_FinalFlag_sub")]
        public PrideTypes FinalFlag { get; set; } = PrideTypes.Default;
    }
}
