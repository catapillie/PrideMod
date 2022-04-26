using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using System;

namespace Celeste.Mod.PrideMod.UI {
    public abstract class AbstractPrideSlider : TextMenu.Option<int> {
        public abstract bool PerformCustomRendering { get; }

        protected AbstractPrideSlider(string label, Func<PrideTypes, string> values, PrideTypes value)
            : base(label) {
            int val = (int)value;
            for (int i = 0; i < PrideData.PrideCount; i++)
                Add(values((PrideTypes)i), i, val == i);
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

        public abstract void CustomRender(Vector2 position);
    }
}
