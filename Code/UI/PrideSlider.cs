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

        private readonly string[] spriteTypes, anims, defaultSprites, defaultAnims;
        private readonly float[] offsets;
        private readonly Sprite[] sprites;

        internal PrideSlider(string label, PreviewSpriteAttribute[] sprites, Func<PrideTypes, string> values, int min, int max, int value = -1)
            : base(label) {
            for (int i = min; i <= max; i++)
                Add(values((PrideTypes)i), i, value == i);

            this.sprites = new Sprite[sprites.Length];
            spriteTypes = new string[sprites.Length];
            anims = new string[sprites.Length];
            defaultSprites = new string[sprites.Length];
            defaultAnims = new string[sprites.Length];
            offsets = new float[sprites.Length];

            for (int i = 0; i < sprites.Length; i++) {
                spriteTypes[i] = sprites[i].SpriteType;
                anims[i] = sprites[i].Anim;
                defaultSprites[i] = sprites[i].DefaultSprite;
                defaultAnims[i] = sprites[i].DefaultAnim;
                offsets[i] = sprites[i].Offset;
            }

            RecreatePreviewSprites();
        }

        public override void LeftPressed() {
            int prev = Index;
            base.LeftPressed();
            if (Index != prev)
                RecreatePreviewSprites();
        }

        public override void RightPressed() {
            int prev = Index;
            base.RightPressed();
            if (Index != prev)
                RecreatePreviewSprites();
        }

        private void RecreatePreviewSprites() {
            for (int i = 0; i < sprites.Length; i++) {
                string spriteType = spriteTypes[i];
                string anim = anims[i];
                string defaultSprite = defaultSprites[i];
                string defaultAnim = defaultAnims[i];

                if (spriteType != null) {
                    string id = PrideType.GetCustomSpriteID(spriteType, defaultSprite);

                    (sprites[i] = GFX.SpriteBank.Create(id))
                        .Play(id == defaultSprite ? defaultAnim : anim);
                }
            }
        }

        public override void Update() {
            base.Update();

            if (Container.Current == this) {
                for (int i = 0; i < sprites.Length; i++)
                    sprites[i].Update();
            }
        }

        public override void Render(Vector2 position, bool highlighted) {
            base.Render(position, highlighted);

            if (Container.Current == this) {
                Draw.SpriteBatch.End();
                SamplerState oldSamplerState = Draw.SpriteBatch.GraphicsDevice.SamplerStates[0];

                Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

                for (int i = 0; i < sprites.Length; i++) {
                    Sprite sprite = sprites[i];
                    float offset = offsets[i];

                    if (sprite.Texture != null)
                        sprite.Texture.DrawJustified(position + new Vector2(Container.Width + 100 + offset, 0), sprite.Justify ?? Vector2.One * 0.5f, Color.White, 6f);
                }

                Draw.SpriteBatch.End();

                Draw.SpriteBatch.GraphicsDevice.SamplerStates[0] = oldSamplerState;
                Draw.SpriteBatch.Begin();
            }
        }
    }
}
