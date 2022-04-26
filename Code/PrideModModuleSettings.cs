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
        [PrideSetting("modoptions_PrideMod_ASideCrystalHeart", "crystalheart", header: "modoptions_PrideMod_header_CrystalHearts")]
        public PrideTypes ASideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_BSideCrystalHeart", "crystalheart")]
        public PrideTypes BSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_CSideCrystalHeart", "crystalheart")]
        public PrideTypes CSideCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_EmptyCrystalHeart", "crystalheart")]
        public PrideTypes EmptyCrystalHeart { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GhostCrystalHeart", "crystalheart")]
        public PrideTypes GhostCrystalHeart { get; set; } = PrideTypes.Default;


        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_BeginnerMiniHeart", "miniheart", header: "modoptions_PrideMod_header_MiniHearts")]
        public PrideTypes BeginnerMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_IntermediateMiniHeart", "miniheart")]
        public PrideTypes IntermediateMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_AdvancedMiniHeart", "miniheart")]
        public PrideTypes AdvancedMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_ExpertMiniHeart", "miniheart")]
        public PrideTypes ExpertMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_GrandmasterMiniHeart", "miniheart")]
        public PrideTypes GrandmasterMiniHeart { get; set; } = PrideTypes.Default;
        
        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_GhostMiniHeart", "miniheart")]
        public PrideTypes GhostMiniHeart { get; set; } = PrideTypes.Default;


        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_Strawberry", "strawberry", header: "modoptions_PrideMod_header_Strawberries")]
        public PrideTypes Strawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GhostStrawberry", "ghostberry")]
        public PrideTypes GhostStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GoldenStrawberry", "goldenberry")]
        public PrideTypes GoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_GhostGoldenStrawberry", "ghostgoldenberry")]
        public PrideTypes GhostGoldenStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_SilverStrawberry", "silverberry")]
        public PrideTypes SilverStrawberry { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [CollabUtils2_PrideSetting("modoptions_PrideMod_GhostSilverStrawberry", "ghostsilverberry")]
        public PrideTypes GhostSilverStrawberry { get; set; } = PrideTypes.Default;


        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_SummitFlag", null, header: "modoptions_PrideMod_header_Flags")]
        public PrideTypes SummitFlag { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_FinalFlag", null)]
        public PrideTypes FinalFlag { get; set; } = PrideTypes.Default;

        [SettingIgnore]
        [PrideSetting("modoptions_PrideMod_Confetti", null)]
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
        public readonly string SpriteType;
        public readonly string Header;

        public PrideSettingAttribute(string name, string spriteType, string header = null) {
            Name = name;
            SpriteType = spriteType;
            Header = header;
        }

        public virtual bool Shown() => true;
    }

    public class CollabUtils2_PrideSettingAttribute : PrideSettingAttribute {
        public CollabUtils2_PrideSettingAttribute(string name, string spriteType, string header = null)
            : base(name, spriteType, header) { }

        public override bool Shown() => Dependencies.CollabUtils2_Loaded;
    }
}
