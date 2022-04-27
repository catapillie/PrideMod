using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste.Mod.PrideMod.UI {
    public class PrideSliderBase : TextMenu.Option<int> {
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
                SamplerState oldSamplerState = Draw.SpriteBatch.GraphicsDevice.SamplerStates[0];

                Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

                CustomRender(position);

                Draw.SpriteBatch.End();

                Draw.SpriteBatch.GraphicsDevice.SamplerStates[0] = oldSamplerState;
                Draw.SpriteBatch.Begin();
            }
        }

        public virtual void SelectedDifferentValue() { }

        public virtual void CustomRender(Vector2 position) { }
    }
}
