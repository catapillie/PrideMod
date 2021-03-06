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
        public readonly bool DoesTheConfetti;

        public PrideModSettingInfo(PropertyInfo property,
                                   PrideSettingAttribute attribute,
                                   IEnumerable<PreviewSpriteAttribute> previewSpriteAttributes,
                                   DoesTheConfettiAttribute confettiAttribute) {
            Property = property;
            Attribute = attribute;
            PreviewSpritesAttributes = previewSpriteAttributes.ToArray();
            DoesTheConfetti = confettiAttribute != null;
        }
    }

    [SettingName("modoptions_PrideMod")]
    public class PrideModModuleSettings : EverestModuleSettings {
        public static readonly PrideModSettingInfo[] Info
            = typeof(PrideModModuleSettings).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                            .Where(p => p.PropertyType == typeof(Pride))
                                            .Select(p => new PrideModSettingInfo(
                                                p, p.GetCustomAttribute<PrideSettingAttribute>(),
                                                p.GetCustomAttributes<PreviewSpriteAttribute>(),
                                                p.GetCustomAttribute<DoesTheConfettiAttribute>()))
                                            .Where(info => info.Attribute != null).ToArray();

        [SettingName("modoptions_PrideMod_Enabled")]
        [SettingSubText("modoptions_PrideMod_Enabled_sub")]
        public bool Enabled { get; set; } = true;

        [SettingName("modoptions_PrideMod_MinimalBloom")]
        [SettingSubText("modoptions_PrideMod_MinimalBloom_sub")]
        public bool MinimalBloom { get; set; } = true;

        [YamlIgnore]
        public int GlobalPride { get; private set; } = 0;
        public void CreateGlobalPrideEntry(TextMenu menu, bool _) {
            GlobalPrideButton button = new();
            menu.Add(button);
            button.AddDescription(menu, Dialog.Clean("modoptions_PrideMod_GlobalPride_sub"));
        }

        [YamlIgnore]
        public int PrideSettings { get; private set; } = 0;
        public void CreatePrideSettingsEntry(TextMenu menu, bool inGame) {
            var item = AbstractSubmenu.BuildOpenMenuButton<OuiPrideSettings>(
                menu,
                inGame,
                () => OuiModOptions.Instance.Overworld.Goto<OuiModOptions>(),
                new object[0]
            ); // :>
            menu.Add(item);
            item.AddDescription(menu, Dialog.Clean("modoptions_PrideMod_PrideSettings_sub"));
        }

        #region Crystal Hearts

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_ASideCrystalHeart", header: "modoptions_PrideMod_header_CrystalHearts")]
        [PreviewSprite(
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem0", defaultAnim: "fastspin",
            globalOffsetX: 0, globalOffsetY: 0
        )]
        public Pride ASideCrystalHeart { get; set; } = Pride.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_BSideCrystalHeart")]
        [PreviewSprite(
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem1", defaultAnim: "fastspin",
            globalOffsetX: 20, globalOffsetY: 0
        )]
        public Pride BSideCrystalHeart { get; set; } = Pride.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_CSideCrystalHeart")]
        [PreviewSprite(
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem2", defaultAnim: "fastspin",
            globalOffsetX: 40, globalOffsetY: 0
        )]
        public Pride CSideCrystalHeart { get; set; } = Pride.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_EmptyCrystalHeart")]
        [PreviewSprite(
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem3", defaultAnim: "fastspin",
            globalOffsetX: 60, globalOffsetY: 0
        )]
        public Pride EmptyCrystalHeart { get; set; } = Pride.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GhostCrystalHeart")]
        [PreviewSprite(
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartGemGhost", defaultAnim: "fastspin",
            globalOffsetX: 80, globalOffsetY: 0
        )]
        public Pride GhostCrystalHeart { get; set; } = Pride.Default;

        #endregion

        #region Mini Hearts

        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_BeginnerMiniHeart", header: "modoptions_PrideMod_header_MiniHearts")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_beginnerminiheart_default", defaultAnim: "fastspin",
            globalOffsetX: 0, globalOffsetY: 20
        )]
        public Pride BeginnerMiniHeart { get; set; } = Pride.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_IntermediateMiniHeart")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_intermediateminiheart_default", defaultAnim: "fastspin",
            globalOffsetX: 20, globalOffsetY: 20
        )]
        public Pride IntermediateMiniHeart { get; set; } = Pride.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_AdvancedMiniHeart")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_advancedminiheart_default", defaultAnim: "fastspin",
            globalOffsetX: 40, globalOffsetY: 20
        )]
        public Pride AdvancedMiniHeart { get; set; } = Pride.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_ExpertMiniHeart")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_expertminiheart_default", defaultAnim: "fastspin",
            globalOffsetX: 60, globalOffsetY: 20
        )]
        public Pride ExpertMiniHeart { get; set; } = Pride.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_GrandmasterMiniHeart")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_grandmasterminiheart_default", defaultAnim: "fastspin",
            globalOffsetX: 80, globalOffsetY: 20
        )]
        public Pride GrandmasterMiniHeart { get; set; } = Pride.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_GhostMiniHeart")]
        [PreviewSprite(
            spriteType: "miniheart", anim: "fastspin",
            defaultSprite: "PrideMod_ghostminiheart_default", defaultAnim: "fastspin",
            globalOffsetX: 100, globalOffsetY: 20
        )]
        public Pride GhostMiniHeart { get; set; } = Pride.Default;

        #endregion

        #region Strawberries

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_Strawberry", header: "modoptions_PrideMod_header_Strawberries")]
        [PreviewSprite(
            spriteType: "strawberry", anim: "idle",
            defaultSprite: "strawberry", defaultAnim: "idle",
            globalOffsetX: 1, globalOffsetY: 40
        )]
        [PreviewSprite(
            spriteType: "strawberryseed", anim: "idle",
            defaultSprite: "strawberrySeed", defaultAnim: "idle",
            globalOffsetX: 2, globalOffsetY: 58,
            individualOffset: 14
        )]
        public Pride Strawberry { get; set; } = Pride.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GhostStrawberry")]
        [PreviewSprite(
            spriteType: "ghostberry", anim: "idle",
            defaultSprite: "ghostberry", defaultAnim: "idle",
            globalOffsetX: 21, globalOffsetY: 40
        )]
        [PreviewSprite(
            spriteType: "ghostberryseed", anim: "idle",
            defaultSprite: "ghostberrySeed", defaultAnim: "idle",
            globalOffsetX: 22, globalOffsetY: 58,
            individualOffset: 14
        )]
        public Pride GhostStrawberry { get; set; } = Pride.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GoldenStrawberry")]
        [PreviewSprite(
            spriteType: "goldenberry", anim: "idle",
            defaultSprite: "goldberry", defaultAnim: "idle",
            globalOffsetX: 41, globalOffsetY: 40
        )]
        public Pride GoldenStrawberry { get; set; } = Pride.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GhostGoldenStrawberry")]
        [PreviewSprite(
            spriteType: "ghostgoldenberry", anim: "idle",
            defaultSprite: "goldghostberry", defaultAnim: "idle",
            globalOffsetX: 61, globalOffsetY: 40
        )]
        public Pride GhostGoldenStrawberry { get; set; } = Pride.Default;

        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_SilverStrawberry")]
        [PreviewSprite(
            spriteType: "silverberry", anim: "idle",
            defaultSprite: "CollabUtils2_silverBerry", defaultAnim: "idle",
            globalOffsetX: 41, globalOffsetY: 60
        )]
        public Pride SilverStrawberry { get; set; } = Pride.Default;

        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_GhostSilverStrawberry")]
        [PreviewSprite(
            spriteType: "ghostsilverberry", anim: "idle",
            defaultSprite: "CollabUtils2_ghostSilverBerry", defaultAnim: "idle",
            globalOffsetX: 61, globalOffsetY: 60
        )]
        public Pride GhostSilverStrawberry { get; set; } = Pride.Default;

        #endregion

        #region Cassettes

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_Cassette", header: "modoptions_PrideMod_header_Cassettes")]
        [PreviewSprite(
            spriteType: "cassette", anim: "fastspin",
            defaultSprite: "PrideMod_cassette_default", defaultAnim: "fastspin",
            globalOffsetX: 84, globalOffsetY: 32
        )]
        public Pride Cassette { get; set; } = Pride.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GhostCassette")]
        [PreviewSprite(
            spriteType: "cassette", anim: "fastspin",
            defaultSprite: "PrideMod_ghostcassette_default", defaultAnim: "fastspin",
            globalOffsetX: 84, globalOffsetY: 52
        )]
        public Pride GhostCassette { get; set; } = Pride.Default;

        #endregion

        #region Flags

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_SummitFlag", header: "modoptions_PrideMod_header_Flags")]
        [PreviewSprite(
            spriteType: "summitflag", anim: "idle",
            defaultSprite: "PrideMod_summitflag_default", defaultAnim: "idle",
            globalOffsetX: 112, globalOffsetY: 4
        )]
        public Pride SummitFlag { get; set; } = Pride.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_FinalFlag")]
        [PreviewSprite(
            spriteType: "finalflag", anim: "idle",
            defaultSprite: "PrideMod_finalflag_default", defaultAnim: "idle",
            globalOffsetX: 148, globalOffsetY: 44
        )]
        public Pride FinalFlag { get; set; } = Pride.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_Confetti")]
        [DoesTheConfetti]
        public Pride Confetti { get; set; } = Pride.Default;

        #endregion

        /// <summary>
        /// Checks all the pride settings and finds a common value.
        /// </summary>
        /// <returns>The common pride setting value, or <see cref="Pride.Default"/> if at least two are not equal.</returns>
        public static Pride GetGlobalPride() {
            var prides = Info.Select(p => p.Property.GetValue(PrideModModule.Settings)).Cast<Pride>();

            Pride first = prides.First();
            return prides.All(pride => pride == first) ? first : Pride.Default;
        }
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

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PreviewSpriteAttribute : Attribute {
        public readonly string SpriteType, Anim, DefaultSprite, DefaultAnim;
        public readonly float IndividualOffset;
        public readonly float GlobalOffsetX;
        public readonly float GlobalOffsetY;

        public PreviewSpriteAttribute(string spriteType, string anim,
                                      string defaultSprite, string defaultAnim,
                                      float globalOffsetX, float globalOffsetY,
                                      float individualOffset = 0f) {
            SpriteType = spriteType;
            Anim = anim;
            DefaultSprite = defaultSprite;
            DefaultAnim = defaultAnim;
            GlobalOffsetX = globalOffsetX;
            GlobalOffsetY = globalOffsetY;
            IndividualOffset = individualOffset;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DoesTheConfettiAttribute : Attribute { }

    #endregion
}
