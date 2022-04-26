using System;

namespace Celeste.Mod.PrideMod.UI {
    public class OuiPrideSettings : AbstractSubmenu {
        public OuiPrideSettings()
            : base("modoptions_PrideMod_PrideSettings_title", "modoptions_PrideMod_PrideSettings") { }

        protected override void addOptionsToMenu(TextMenu menu, bool inGame, object[] parameters) {
            PrideModModuleSettings settings = PrideModModule.Settings;

            bool first = true;
            foreach (var info in PrideModModuleSettings.Info) {
                var prop = info.Property;
                var attr = info.Attribute;
                var sprites = info.PreviewSpritesAttributes;

                if (attr.Shown()) {
                    if (attr.Header != null)
                        CreateSubHeader(menu, attr.Header);

                    PrideTypes value = (PrideTypes)prop.GetValue(settings);
                    void action(PrideTypes prideType) => prop.SetValue(settings, prideType);

                    string name = Dialog.Clean(attr.Name);
                    string desc = Dialog.Clean(attr.Name + "_sub");

                    PrideSliderBase item;
                    if (sprites.Length > 0)
                        item = CreatePrideSetting(name, sprites, value, action, displayDesc: first);
                    else
                        item = new(name, i => i.GetFormattedName(), value);

                    Console.WriteLine(item.GetType());

                    item.Change(i => action((PrideTypes)i));

                    menu.Add(item);
                    item.AddDescription(menu, desc);

                    // The first TextMenu.Item in the submenu would not display its description until reselected
                    // so we want to show it anyway
                    if (first)
                        item.OnEnter();

                    first = false;
                }
            }
        }

        private PreviewedPrideSlider CreatePrideSetting(string settingName, PreviewSpriteAttribute[] sprites, PrideTypes value, Action<PrideTypes> action, bool displayDesc = false)
            => new(settingName, sprites, i => i.GetFormattedName(), value);

        private static void CreateSubHeader(TextMenu menu, string header)
            => menu.Add(new TextMenu.SubHeader(Dialog.Clean(header)));

        protected override void gotoMenu(Overworld overworld)
            => overworld.Goto<OuiPrideSettings>();
    }
}