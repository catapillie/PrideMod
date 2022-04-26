using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using System;

namespace Celeste.Mod.PrideMod.UI {
    public class PrideSlider : TextMenu.Option<int> {
        public PrideTypes PrideType {
            get => (PrideTypes)Index;
            set => Index = (int)value;
        }

        private readonly string spriteType;
        private Sprite sprite;

        public PrideSlider(string label, string spriteType, Func<PrideTypes, string> values, int min, int max, int value = -1)
            : base(label) {
            for (int i = min; i <= max; i++)
                Add(values((PrideTypes)i), i, value == i);

            this.spriteType = spriteType;
            RecreatePreviewSprite();
        }

        public override void LeftPressed() {
            int prev = Index;
            base.LeftPressed();
            if (Index != prev)
                RecreatePreviewSprite();
        }

        public override void RightPressed() {
            int prev = Index;
            base.RightPressed();
            if (Index != prev)
                RecreatePreviewSprite();
        }

        private void RecreatePreviewSprite() {
            if (spriteType != null) {
                sprite = GFX.SpriteBank.Create(PrideType.GetCustomSpriteID(spriteType, "strawberry"));
                sprite.Play("idle");
            }
        }

        public override void Update() {
            base.Update();

            if (Container.Current == this && Container.Focused)
                sprite?.Update();
        }

        public override void Render(Vector2 position, bool highlighted) {
            base.Render(position, highlighted);

            if (highlighted) {
                Draw.SpriteBatch.End();
                SamplerState oldSamplerState = Draw.SpriteBatch.GraphicsDevice.SamplerStates[0];

                Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone);

                if (sprite != null && sprite.Texture != null) {
                    sprite.Texture.DrawCentered(position + new Vector2(Container.Width + 100, 0), Color.White, 6f);
                }

                Draw.SpriteBatch.End();

                Draw.SpriteBatch.GraphicsDevice.SamplerStates[0] = oldSamplerState;
                Draw.SpriteBatch.Begin();
            }
        }
    }
}
