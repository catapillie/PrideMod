﻿using System;

namespace Celeste.Mod.PrideMod.UI {
    public class OuiPrideSettings : AbstractSubmenu {
        public OuiPrideSettings()
            : base("modoptions_PrideMod_PrideSettings_title", "modoptions_PrideMod_PrideSettings") { }

        protected override void addOptionsToMenu(TextMenu menu, bool inGame, object[] parameters) {
            PrideModModuleSettings settings = PrideModModule.Settings;

            bool first = true;
            foreach (var info in PrideModModuleSettings.Info) {
                var prop = info.Key;
                var attr = info.Value;

                if (attr.Shown()) {
                    if (attr.Header != null)
                        CreateSubHeader(menu, attr.Header);

                    PrideTypes value = (PrideTypes)prop.GetValue(settings);
                    void action(PrideTypes prideType) => prop.SetValue(settings, prideType);
                    CreatePrideSetting(menu, attr.Name, attr.SpriteType, attr.Anim, attr.DefaultSprite, attr.DefaultAnim, value, action, displayDesc: first);

                    first = false;
                }
            }
        }

        private void CreatePrideSetting(TextMenu menu, string settingName, string spriteType, string anim, string defaultSprite, string defaultAnim, PrideTypes value, Action<PrideTypes> action, bool displayDesc = false) {
            PrideSlider item = new PrideSlider(
                Dialog.Clean(settingName),
                spriteType, anim,
                defaultSprite, defaultAnim,
                i => i.GetFormattedName(),
                0, PrideData.PrideCount - 1,
                (int)value
            ).Change(i => action((PrideTypes)i)) as PrideSlider;

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