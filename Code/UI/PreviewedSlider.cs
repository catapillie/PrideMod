using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using System;

namespace Celeste.Mod.PrideMod.UI {
    public class PreviewedSlider : TextMenu.Slider {
        public PreviewedSlider(string label, Func<int, string> values, int min, int max, int value = -1)
            : base(label, values, min, max, value) { }

        public override void Render(Vector2 position, bool highlighted) {
            base.Render(position, highlighted);

            if (highlighted) {
                Draw.SpriteBatch.End();
                SamplerState oldSamplerState = Draw.SpriteBatch.GraphicsDevice.SamplerStates[0];

                Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone);

                GFX.Game[PrideData.GetCustomTexturePath(PrideTypes.Transgender, "crystalheart", "00", "")].DrawCentered(position + Vector2.UnitX * (Container.Width + 100), Color.White, 6f);

                Draw.SpriteBatch.End();

                Draw.SpriteBatch.GraphicsDevice.SamplerStates[0] = oldSamplerState;
                Draw.SpriteBatch.Begin();
            }
        }
    }
}
