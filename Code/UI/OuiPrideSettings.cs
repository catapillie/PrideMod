namespace Celeste.Mod.PrideMod.UI {
    public class OuiPrideSettings : AbstractSubmenu {
        public OuiPrideSettings()
            : base("modoptions_PrideMod_PrideSettings_title", "modoptions_PrideMod_PrideSettings") { }

        protected override void addOptionsToMenu(TextMenu menu, bool inGame, object[] parameters) {
            PrideModModuleSettings settings = PrideModModule.Settings;

            bool first = true;
            float yOffset = 0f;

            foreach (var info in PrideModModuleSettings.Info) {
                var prop = info.Property;
                var attr = info.Attribute;

                if (attr.Shown()) {
                    var sprites = info.PreviewSpritesAttributes;

                    if (attr.Header != null) {
                        var subHeader = new TextMenu.SubHeader(Dialog.Clean(attr.Header));
                        menu.Add(subHeader);
                        yOffset += subHeader.Height() + menu.ItemSpacing;
                    }

                    Pride value = (Pride)prop.GetValue(settings);
                    void action(Pride prideType) => prop.SetValue(settings, prideType);

                    string name = Dialog.Clean(attr.Name);
                    string desc = Dialog.Clean(attr.Name + "_sub");

                    PrideSliderBase item;
                    if (sprites.Length > 0)
                        item = new PreviewedPrideSlider(name, value, sprites);
                    else if (info.DoesTheConfetti)
                        item = new ConfettiPrideSlider(name, value, yOffset);
                    else
                        item = new(name, value);

                    item.Change(i => action((Pride)i));

                    menu.Add(item);
                    item.AddDescription(menu, desc);

                    // The first TextMenu.Item in the submenu would not display its description until reselected
                    // so we want to show it anyway
                    if (first)
                        item.OnEnter();

                    first = false;
                    yOffset += item.Height() + menu.ItemSpacing; 
                }
            }
        }

        protected override void gotoMenu(Overworld overworld)
            => overworld.Goto<OuiPrideSettings>();
    }
}