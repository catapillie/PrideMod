using Celeste.Mod.PrideMod.Components;

namespace Celeste.Mod.PrideMod.UI {
    public class GlobalPrideButton : TextMenu.Button {
        private GlobalPrideSettingComponent component;

        private bool Opened => !Container.Focused && Container.Current == this && component != null;

        public GlobalPrideButton()
            : base(Dialog.Clean("modoptions_PrideMod_GlobalPride")) {
        }

        public override void ConfirmPressed() {
            base.ConfirmPressed();

            if (component == null)
                Container.Add(component = new GlobalPrideSettingComponent(Container));
            component.Show();

            Input.MenuConfirm.ConsumePress();
        }

        public override void Update() {
            base.Update();

            if (Opened) {
                if (Input.MenuConfirm.Pressed)
                    component.Confirm();
                else if (Input.MenuCancel.Pressed)
                    component.Cancel();
            }
        }

        #region Hooks

        internal static void Hook() {
            On.Celeste.TextMenu.Render += Mod_TextMenu_Render;
        }

        internal static void Unhook() {
            On.Celeste.TextMenu.Render -= Mod_TextMenu_Render;
        }

        private static void Mod_TextMenu_Render(On.Celeste.TextMenu.orig_Render orig, TextMenu self) {
            orig(self);

            GlobalPrideSettingComponent component = self.Get<GlobalPrideSettingComponent>();
            if (component != null && component.Visible)
                component.Display();
        }

        #endregion
    }
}
