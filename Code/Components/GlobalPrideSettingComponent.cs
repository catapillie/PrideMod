using Celeste.Mod.PrideMod.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using System;
using System.Linq;

namespace Celeste.Mod.PrideMod.Components {
    public class GlobalPrideSettingComponent : Component {
        public const int GLOBAL_PREVIEW_UI_SCALE = 5;

        private class Entry {
            public readonly int Index;
            public readonly PrideTypes Pride;
            public readonly string Name;

            public readonly Wiggler Wiggler = Wiggler.Create(0.25f, 3f);

            private float l;
            public float Lerp { get; private set; }

            public readonly float Width;

            public Entry(int i) {
                Name = (Pride = (PrideTypes)i).GetFormattedName();
                Width = ActiveFont.WidthToNextLine(Name, 0);
            }

            public void Update(bool selected) {
                Wiggler.Update();

                l = Calc.Approach(l, selected ? 1 : 0, Engine.DeltaTime * 4f);
                Lerp = Ease.QuadInOut(l);
            }
        }

        private class Preview {
            public Sprite Sprite;
            public readonly PreviewSpriteAttribute SpriteInfo;
            public readonly float OffsetX, OffsetY;

            public Preview(PreviewSpriteAttribute spriteInfo) {
                SpriteInfo = spriteInfo;
                OffsetX = spriteInfo.GlobalOffsetX * GLOBAL_PREVIEW_UI_SCALE;
                OffsetY = spriteInfo.GlobalOffsetY * GLOBAL_PREVIEW_UI_SCALE;
            }
        }

        private float lerp;
        private bool shown;

        private readonly string title = Dialog.Clean("modoptions_PrideMod_GlobalPride_title");

        private readonly Entry[] entries = new Entry[PrideData.PrideCount];
        private readonly Preview[] previews;
        private float previewYOffset;

        private int index;

        private float y;
        private float TargetY => index * ActiveFont.LineHeight;

        private readonly float halfMaxWidth;

        private readonly TextMenu menu;

        public GlobalPrideSettingComponent(TextMenu menu)
            : base(active: true, visible: true) {
            for (int i = 0; i < entries.Length; i++) {
                Entry entry = entries[i] = new(i);
                if (halfMaxWidth < entry.Width)
                    halfMaxWidth = entry.Width;
            }

            halfMaxWidth = halfMaxWidth / 2f + 30;

            this.menu = menu;

            previews = PrideModModuleSettings.Info.Where(info => info.Attribute.Shown())
                                                  .SelectMany(info => info.PreviewSpritesAttributes)
                                                  .Select(spriteInfo => new Preview(spriteInfo))
                                                  .ToArray();
        }

        public override void Update() {
            base.Update();

            Visible = lerp > 0;

            lerp = Calc.Approach(lerp, shown ? 1 : 0, Engine.DeltaTime * 4);

            if (shown) {
                int prev = index;
                bool down;
                if (down = Input.MenuDown.Pressed)
                    ++index;
                else if (Input.MenuUp.Pressed)
                    --index;
                index = Util.Mod(index, PrideData.PrideCount);

                if (index != prev)
                    SelectedDifferentValue(down);
            }

            if (Visible) {
                y += (TargetY - y) * (1f - (float)Math.Pow(0.0099999997764825821, Engine.RawDeltaTime));

                foreach (Entry entry in entries)
                    entry.Update(entry.Index == index);

                foreach (Preview preview in previews)
                    preview.Sprite?.Update();
            }
        }

        public void Show() {
            shown = true;
            index = (int)PrideModModuleSettings.GetGlobalPride();
            y = TargetY;
            menu.Focused = false;

            RecreateSprites();
        }

        public void Hide() {
            shown = false;
            menu.Focused = true;
        }

        private void SelectedDifferentValue(bool down) {
            RecreateSprites();

            entries[index].Wiggler.Start();
            Audio.Play(down ? SFX.ui_main_roll_down : SFX.ui_main_roll_up);
        }

        public void Confirm() {
            Hide();

            foreach (var info in PrideModModuleSettings.Info)
                info.Property.SetValue(PrideModModule.Settings, (PrideTypes)index);
            PrideModModule.Instance.SaveSettings();

            Audio.Play(SFX.ui_main_savefile_delete);
        }

        public void Cancel() {
            Hide();
            Audio.Play(SFX.ui_main_button_back);
        }

        private void RecreateSprites() {
            float ymin = float.MaxValue;
            float ymax = float.MinValue;

            PrideTypes pride = (PrideTypes)index;
            foreach (Preview preview in previews) {
                var info = preview.SpriteInfo;

                string id = pride.GetCustomSpriteID(info.SpriteType, info.DefaultSprite);
                string anim = id == info.DefaultSprite ? info.DefaultAnim : info.Anim;

                (preview.Sprite = GFX.SpriteBank.Create(id))
                    .Play(anim);

                if (preview.OffsetY < ymin)
                    ymin = preview.OffsetY;

                float bottom = preview.OffsetY + preview.Sprite.Texture.Height * GLOBAL_PREVIEW_UI_SCALE;
                if (bottom > ymax)
                    ymax = bottom;
            }

            previewYOffset = (ymin + ymax) / 2f;
        }

        internal void Display() {
            float alphaCubed = lerp * lerp * lerp;

            Vector2 mid = new(480, 540);

            Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * Ease.CubeOut(lerp) * 0.95f);

            Color selectedColor = Color.White;
            for (int i = 0; i < entries.Length; i++) {
                Entry entry = entries[i];

                float wiggle = entry.Wiggler.Value * 8f;
                Vector2 pos = mid + new Vector2(0, i * ActiveFont.LineHeight - y + wiggle);

                float top = pos.Y - ActiveFont.LineHeight / 2f;
                float bottom = pos.Y + ActiveFont.LineHeight / 2f;

                if (bottom > 0f && top < Engine.Height) {
                    Color color = Color.White;

                    if (i == index) {
                        if (Settings.Instance.DisableFlashes)
                            color = TextMenu.HighlightColorA;
                        else
                            color = entry.Pride == PrideTypes.Default ?
                                Util.MultiColorLerp(Entity.Scene.TimeActive * 10f, PrideSliderBase.DefaultHighlightColors) :
                                Util.MultiColorPingPong(Entity.Scene.TimeActive * 2f, PrideData.PrideColors[entry.Pride]);

                        GFX.Gui["dot_outline"].DrawCentered(pos - new Vector2(halfMaxWidth, 0), color * lerp);

                        selectedColor = color;
                    }

                    ActiveFont.DrawOutline(entry.Name, pos, new(0.5f), Vector2.One, color * lerp, 2f, Color.Black * alphaCubed);
                }
            }

            float ease = Ease.ExpoOut(lerp);
            float w = 970f * Ease.SineOut(lerp);    
            float atY = -10 + 200 * ease;
            Draw.Rect(-10, -10, 1940, 200 * ease, Color.Black * lerp);
            Draw.Rect(960 - w, atY, w * 2, 3f, Color.White * lerp);
            ActiveFont.DrawOutline(title, new(960, 100 * ease), new(0.5f), Vector2.One * 1.5f, Color.White * lerp, 4, Color.Black * alphaCubed);

            #region Sprite Preview

            GFX.Gui["dotarrow_outline"].DrawCentered(mid + new Vector2(halfMaxWidth + 50, 0), selectedColor * lerp);

            Vector2 from = mid + new Vector2(halfMaxWidth + 120, 0);

            Draw.SpriteBatch.End();
            SamplerState oldSamplerState = Draw.SpriteBatch.GraphicsDevice.SamplerStates[0];

            Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

            foreach (Preview preview in previews) {
                Sprite sprite = preview.Sprite;
                MTexture texture = sprite.Texture;

                if (sprite != null && texture != null) {
                    Vector2 offset = new(preview.OffsetX, preview.OffsetY - previewYOffset);
                    sprite.Texture.Draw(from + offset, Vector2.Zero, Color.White * lerp, GLOBAL_PREVIEW_UI_SCALE);
                }
            }

            Draw.SpriteBatch.End();

            Draw.SpriteBatch.GraphicsDevice.SamplerStates[0] = oldSamplerState;
            Draw.SpriteBatch.Begin();

            #endregion
        }
    }
}
