using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste.Mod.PrideMod.UI {
    public class PrideSliderBase : TextMenu.Option<int> {
        public static readonly Color[] DefaultHighlightColors = new[] {
            TextMenu.HighlightColorA,
            TextMenu.HighlightColorB,
        };

        public const int PRIDE_SETTINGS_UI_SCALE = 6;

        public PrideTypes PrideType {
            get => (PrideTypes)Index;
            set => Index = (int)value;
        }

        public virtual bool PerformCustomRendering => false;

        public PrideSliderBase(string label, PrideTypes value)
            : base(label) {
            int val = (int)value;
            for (int i = 0; i < PrideData.PrideCount; i++)
                Add(((PrideTypes)i).GetFormattedName(), i, val == i);
        }

        public override void LeftPressed() {
            int prev = Index;

            base.LeftPressed();

            if (Index != prev)
                SelectedDifferentValue();
        }

        public override void RightPressed() {
            int prev = Index;

            base.RightPressed();

            if (Index != prev)
                SelectedDifferentValue();
        }

        public override void Render(Vector2 position, bool highlighted) {
            base.Render(position, highlighted);

            if (PerformCustomRendering) {
                Draw.SpriteBatch.End();
                Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Engine.ScreenMatrix);

                CustomRender(position);

                Draw.SpriteBatch.End();
                Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Engine.ScreenMatrix);
            }
        }

        public virtual void SelectedDifferentValue() { }

        public virtual void CustomRender(Vector2 position) { }

        #region Hooks

        internal static void Hook() {
            On.Celeste.TextMenu.Update += Mod_TextMenu_Update;
        }

        internal static void Unhook() {
            On.Celeste.TextMenu.Update -= Mod_TextMenu_Update;
        }

        private static void Mod_TextMenu_Update(On.Celeste.TextMenu.orig_Update orig, TextMenu self) {
            orig(self);

            if (!Settings.Instance.DisableFlashes && self.Current is PrideSliderBase slider) {
                PrideTypes pride = slider.PrideType;
                self.HighlightColor = pride == PrideTypes.Default ?
                    Util.MultiColorLerp(self.Scene.RawTimeActive * 10f, DefaultHighlightColors) :
                    Util.MultiColorPingPong(self.Scene.RawTimeActive * 2f, PrideData.PrideColors[pride]);
            }
        }

        #endregion
    }
}
