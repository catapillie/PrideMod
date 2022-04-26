﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using System;

namespace Celeste.Mod.PrideMod.UI {
    public class PrideSlider : TextMenu.Option<int> {
        public PrideTypes PrideType {
            get => (PrideTypes)Index;
            set => Index = (int)value;
        }

        private readonly string spriteType, anim, defaultSprite, defaultAnim;
        private Sprite sprite;

        public PrideSlider(string label, string spriteType, string anim, string defaultSprite, string defaultAnim, Func<PrideTypes, string> values, int min, int max, int value = -1)
            : base(label) {
            for (int i = min; i <= max; i++)
                Add(values((PrideTypes)i), i, value == i);

            this.spriteType = spriteType;
            this.anim = anim;
            this.defaultSprite = defaultSprite;
            this.defaultAnim = defaultAnim;

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
                string id = PrideType.GetCustomSpriteID(spriteType, defaultSprite);
                sprite = GFX.SpriteBank.Create(id);
                sprite.Play(id == defaultSprite ? defaultAnim : anim);
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

                Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

                if (sprite != null && sprite.Texture != null)
                    sprite.Texture.DrawJustified(position + new Vector2(Container.Width + 100, 0), sprite.Justify ?? Vector2.One * 0.5f, Color.White, 6f);

                Draw.SpriteBatch.End();

                Draw.SpriteBatch.GraphicsDevice.SamplerStates[0] = oldSamplerState;
                Draw.SpriteBatch.Begin();
            }
        }
    }
}
