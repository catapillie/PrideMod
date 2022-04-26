using Celeste.Mod.PrideMod.UI;
using Celeste.Mod.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;

namespace Celeste.Mod.PrideMod {
    [SettingName("modoptions_PrideMod")]
    public class PrideModModuleSettings : EverestModuleSettings {
        public static readonly KeyValuePair<PropertyInfo, PrideSettingAttribute>[] 
            Info = typeof(PrideModModuleSettings).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                 .Where(p => p.PropertyType == typeof(PrideTypes))
                                                 .Select(p => new KeyValuePair<PropertyInfo, PrideSettingAttribute>(p, p.GetCustomAttribute<PrideSettingAttribute>()))
                                                 .Where(kv => kv.Value != null).ToArray();

        public bool Enabled { get; set; } = true;

        [SettingName("modoptions_PrideMod_MinimalBloom")]
        [SettingSubText("modoptions_PrideMod_MinimalBloom_sub")]
        public bool MinimalBloom { get; set; } = true;


        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_ASideCrystalHeart",
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem0", defaultAnim: "fastspin",
            header: "modoptions_PrideMod_header_CrystalHearts"
        )]
        public PrideTypes ASideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_BSideCrystalHeart",
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem1", defaultAnim: "fastspin"
        )]
        public PrideTypes BSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_CSideCrystalHeart",
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem2", defaultAnim: "fastspin"
        )]
        public PrideTypes CSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_EmptyCrystalHeart",
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartgem3", defaultAnim: "fastspin"
        )]
        public PrideTypes EmptyCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_GhostCrystalHeart",
            spriteType: "crystalheart", anim: "fastspin",
            defaultSprite: "heartGemGhost", defaultAnim: "fastspin"
        )]
        public PrideTypes GhostCrystalHeart { get; set; } = PrideTypes.Default;


        [SettingIgnore]
        [CollabUtils2_PrideSetting(
            "modoptions_PrideMod_BeginnerMiniHeart",
            spriteType: "miniheart", anim: "idle",
            defaultSprite: "strawberry", defaultAnim: "idle",
            header: "modoptions_PrideMod_header_MiniHearts"
        )]
        public PrideTypes BeginnerMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting(
            "modoptions_PrideMod_IntermediateMiniHeart",
            spriteType: "miniheart", anim: "idle",
            defaultSprite: "strawberry", defaultAnim: "idle"
        )]
        public PrideTypes IntermediateMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting(
            "modoptions_PrideMod_AdvancedMiniHeart",
            spriteType: "miniheart", anim: "idle",
            defaultSprite: "strawberry", defaultAnim: "idle"
        )]
        public PrideTypes AdvancedMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting(
            "modoptions_PrideMod_ExpertMiniHeart",
            spriteType: "miniheart", anim: "idle",
            defaultSprite: "strawberry", defaultAnim: "idle"
        )]
        public PrideTypes ExpertMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting(
            "modoptions_PrideMod_GrandmasterMiniHeart",
            spriteType: "miniheart", anim: "idle",
            defaultSprite: "strawberry", defaultAnim: "idle"
        )]
        public PrideTypes GrandmasterMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting(
            "modoptions_PrideMod_GhostMiniHeart",
            spriteType: "miniheart", anim: "idle",
            defaultSprite: "strawberry", defaultAnim: "idle"
        )]
        public PrideTypes GhostMiniHeart { get; set; } = PrideTypes.Default;


        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_Strawberry",
            spriteType: "strawberry", anim: "idle",
            defaultSprite: "strawberry", defaultAnim: "idle",
            header: "modoptions_PrideMod_header_Strawberries"
        )]
        public PrideTypes Strawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_GhostStrawberry",
            spriteType: "ghostberry", anim: "idle",
            defaultSprite: "ghostberry", defaultAnim: "idle"
        )]
        public PrideTypes GhostStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_GoldenStrawberry",
            spriteType: "goldenberry", anim: "idle",
            defaultSprite: "goldberry", defaultAnim: "idle"
        )]
        public PrideTypes GoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_GhostGoldenStrawberry",
            spriteType: "ghostgoldenberry", anim: "idle",
            defaultSprite: "goldghostberry", defaultAnim: "idle"
        )]
        public PrideTypes GhostGoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [CollabUtils2_PrideSetting(
            "modoptions_PrideMod_SilverStrawberry",
            spriteType: "silverberry", anim: "idle",
            defaultSprite: "CollabUtils2_silverBerry", defaultAnim: "idle"
        )]
        public PrideTypes SilverStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [CollabUtils2_PrideSetting(
            "modoptions_PrideMod_GhostSilverStrawberry",
            spriteType: "ghostsilverberry", anim: "idle",
            defaultSprite: "CollabUtils2_ghostSilverBerry", defaultAnim: "idle"
        )]
        public PrideTypes GhostSilverStrawberry { get; set; } = PrideTypes.Default;


        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_SummitFlag",
            spriteType: "summitflag", anim: "idle",
            defaultSprite: "PrideMod_summitflag_default", defaultAnim: "idle",
            header: "modoptions_PrideMod_header_Flags"
        )]
        public PrideTypes SummitFlag { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_FinalFlag",
            spriteType: "finalflag", anim: "idle",
            defaultSprite: "PrideMod_finalflag_default", defaultAnim: "idle"
        )]
        public PrideTypes FinalFlag { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting(
            "modoptions_PrideMod_Confetti",
            spriteType: null, anim: null,
            defaultSprite: null, defaultAnim: null
        )]
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


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PrideSettingAttribute : Attribute {
        public readonly string Name;
        public readonly string SpriteType, Anim, DefaultSprite, DefaultAnim;
        public readonly string Header;

        public PrideSettingAttribute(string name, string spriteType, string anim, string defaultSprite, string defaultAnim, string header = null) {
            Name = name;
            SpriteType = spriteType;
            Anim = anim;
            DefaultSprite = defaultSprite;
            DefaultAnim = defaultAnim;
            Header = header;
        }

        public virtual bool Shown() => true;
    }

    public class CollabUtils2_PrideSettingAttribute : PrideSettingAttribute {
        public CollabUtils2_PrideSettingAttribute(string name, string spriteType, string anim, string defaultSprite, string defaultAnim, string header = null)
            : base(name, spriteType, anim, defaultSprite, defaultAnim, header) { }

        public override bool Shown() => Dependencies.CollabUtils2_Loaded;
    }
}
