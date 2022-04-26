using Celeste.Mod.PrideMod.UI;
using Celeste.Mod.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;

namespace Celeste.Mod.PrideMod {
    public class PrideModSettingInfo {
        public readonly PropertyInfo Property;
        public readonly PrideSettingAttribute Attribute;
        public readonly PreviewSpriteAttribute[] PreviewSpritesAttributes;

        public PrideModSettingInfo(PropertyInfo property, PrideSettingAttribute attribute, IEnumerable<PreviewSpriteAttribute> previewSpriteAttributes) {
            Property = property;
            Attribute = attribute;
            PreviewSpritesAttributes = previewSpriteAttributes.ToArray();
        }
    }

    [SettingName("modoptions_PrideMod")]
    public class PrideModModuleSettings : EverestModuleSettings {
        public static readonly PrideModSettingInfo[] Info
            = typeof(PrideModModuleSettings).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                            .Where(p => p.PropertyType == typeof(PrideTypes))
                                            .Select(p => new PrideModSettingInfo(
                                                p, p.GetCustomAttribute<PrideSettingAttribute>(),
                                                p.GetCustomAttributes<PreviewSpriteAttribute>()))
                                            .Where(kv => kv.Attribute != null).ToArray();

        public bool Enabled { get; set; } = true;

        [SettingName("modoptions_PrideMod_MinimalBloom")]
        [SettingSubText("modoptions_PrideMod_MinimalBloom_sub")]
        public bool MinimalBloom { get; set; } = true;

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

        #region Crystal Hearts

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_ASideCrystalHeart", header: "modoptions_PrideMod_header_CrystalHearts")]
        [PreviewSprite(
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem0", defaultAnim: "fastspin"
        )]
        public PrideTypes ASideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_BSideCrystalHeart")]
        [PreviewSprite(
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem1", defaultAnim: "fastspin"
        )]
        public PrideTypes BSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_CSideCrystalHeart")]
        [PreviewSprite(
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem2", defaultAnim: "fastspin"
        )]
        public PrideTypes CSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_EmptyCrystalHeart")]
        [PreviewSprite(
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem3", defaultAnim: "fastspin"
        )]
        public PrideTypes EmptyCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GhostCrystalHeart")]
        [PreviewSprite(
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartGemGhost", defaultAnim: "fastspin"
        )]
        public PrideTypes GhostCrystalHeart { get; set; } = PrideTypes.Default;

        #endregion

        #region Mini Hearts

        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_BeginnerMiniHeart", header: "modoptions_PrideMod_header_MiniHearts")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_beginnerminiheart_default", defaultAnim: "fastspin"
        )]
        public PrideTypes BeginnerMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_IntermediateMiniHeart")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_intermediateminiheart_default", defaultAnim: "fastspin"
        )]
        public PrideTypes IntermediateMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_AdvancedMiniHeart")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_advancedminiheart_default", defaultAnim: "fastspin"
        )]
        public PrideTypes AdvancedMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_ExpertMiniHeart")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_expertminiheart_default", defaultAnim: "fastspin"
        )]
        public PrideTypes ExpertMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_GrandmasterMiniHeart")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_grandmasterminiheart_default", defaultAnim: "fastspin"
        )]
        public PrideTypes GrandmasterMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_GhostMiniHeart")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_ghostminiheart_default", defaultAnim: "fastspin"
        )]
        public PrideTypes GhostMiniHeart { get; set; } = PrideTypes.Default;

        #endregion

        #region Strawberries

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_Strawberry", header: "modoptions_PrideMod_header_Strawberries")]
        [PreviewSprite(
            spriteType: "strawberry", anim: "idle",
            defaultSprite: "strawberry", defaultAnim: "idle"
        )]
        public PrideTypes Strawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GhostStrawberry")]
        [PreviewSprite(
            spriteType: "ghostberry", anim: "idle",
            defaultSprite: "ghostberry", defaultAnim: "idle"
        )]
        public PrideTypes GhostStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GoldenStrawberry")]
        [PreviewSprite(
            spriteType: "goldenberry", anim: "idle",
            defaultSprite: "goldberry", defaultAnim: "idle"
        )]
        public PrideTypes GoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GhostGoldenStrawberry")]
        [PreviewSprite(
            spriteType: "ghostgoldenberry", anim: "idle",
            defaultSprite: "goldghostberry", defaultAnim: "idle"
        )]
        public PrideTypes GhostGoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_SilverStrawberry")]
        [PreviewSprite(
            spriteType: "silverberry", anim: "idle",
            defaultSprite: "CollabUtils2_silverBerry", defaultAnim: "idle"
        )]
        public PrideTypes SilverStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_GhostSilverStrawberry")]
        [PreviewSprite(
            spriteType: "ghostsilverberry", anim: "idle",
            defaultSprite: "CollabUtils2_ghostSilverBerry", defaultAnim: "idle"
        )]
        public PrideTypes GhostSilverStrawberry { get; set; } = PrideTypes.Default;

        #endregion

        #region Flags

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_SummitFlag", header: "modoptions_PrideMod_header_Flags")]
        [PreviewSprite(
            spriteType: "summitflag", anim: "idle",
            defaultSprite: "PrideMod_summitflag_default", defaultAnim: "idle"
        )]
        public PrideTypes SummitFlag { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_FinalFlag")]
        [PreviewSprite(
            spriteType: "finalflag", anim: "idle",
            defaultSprite: "PrideMod_finalflag_default", defaultAnim: "idle"
        )]
        public PrideTypes FinalFlag { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_Confetti")]
        public PrideTypes Confetti { get; set; } = PrideTypes.Default;

        #endregion
    }

    #region Pride Mod Settings Attributes

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PrideSettingAttribute : Attribute {
        public readonly string Name, Header;

        public PrideSettingAttribute(string name, string header = null) {
            Name = name;
            Header = header;
        }

        public virtual bool Shown() => true;
    }

    public class CollabUtils2_PrideSettingAttribute : PrideSettingAttribute {
        public CollabUtils2_PrideSettingAttribute(string name, string header = null)
            : base(name, header) { }

        public override bool Shown() => Dependencies.CollabUtils2_Loaded;
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PreviewSpriteAttribute : Attribute {
        public readonly string SpriteType, Anim, DefaultSprite, DefaultAnim;

        public PreviewSpriteAttribute(string spriteType, string anim, string defaultSprite, string defaultAnim) {
            SpriteType = spriteType;
            Anim = anim;
            DefaultSprite = defaultSprite;
            DefaultAnim = defaultAnim;
        }
    }

    #endregion
}
