using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.PrideMod.UI {
    public class PreviewedPrideSlider : PrideSliderBase {
        public override bool PerformCustomRendering => Selected;

        private class Preview {
            public Sprite Sprite;
            public readonly PreviewSpriteAttribute SpriteInfo;
            public readonly float Offset;

            public Preview(PreviewSpriteAttribute spriteInfo) {
                SpriteInfo = spriteInfo;
                Offset = spriteInfo.IndividualOffset * PRIDE_SETTINGS_UI_SCALE;
            }
        }

        private readonly Preview[] previews;

        private bool Selected => Container.Current == this;

        internal PreviewedPrideSlider(string label, Pride value, PreviewSpriteAttribute[] sprites)
            : base(label, value) {

            previews = new Preview[sprites.Length];

            for (int i = 0; i < sprites.Length; i++)
                previews[i] = new(sprites[i]);

            RecreatePreviewSprites();
        }

        public override void SelectedDifferentValue() => RecreatePreviewSprites();

        private void RecreatePreviewSprites() {
            for (int i = 0; i < previews.Length; i++) {
                var info = previews[i].SpriteInfo;

                string spriteType = info.SpriteType;
                string anim = info.Anim;
                string defaultSprite = info.DefaultSprite;
                string defaultAnim = info.DefaultAnim;

                if (spriteType != null) {
                    string id = Pride.GetCustomSpriteID(spriteType, defaultSprite);

                    (previews[i].Sprite = GFX.SpriteBank.Create(id))
                        .Play(id == defaultSprite ? defaultAnim : anim);
                }
            }
        }

        public override void Update() {
            base.Update();

            if (Selected) {
                foreach (Preview preview in previews)
                    preview.Sprite?.Update();
            }
        }

        public override void CustomRender(Vector2 position) {
            foreach (Preview preview in previews) {
                Sprite sprite = preview.Sprite;
                float offset = preview.Offset;

                if (sprite.Texture != null)
                    sprite.Texture.DrawJustified(position + new Vector2(Container.Width + 100 + offset, 0), sprite.Justify ?? Vector2.One * 0.5f, Color.White, PRIDE_SETTINGS_UI_SCALE);
            }
        }
    }
}
