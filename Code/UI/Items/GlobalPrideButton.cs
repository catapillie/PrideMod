using Microsoft.Xna.Framework;
using Monocle;
using System;

namespace Celeste.Mod.PrideMod.UI {
    public class GlobalPrideButton : TextMenu.Button {
        public class GlobalPrideSettingComponent : Component {
            public class Entry {
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

            private static readonly Color[] defaultHighlightColors = new[] {
                TextMenu.HighlightColorA,
                TextMenu.HighlightColorB
            };

            private float alpha;
            private bool shown;

            private readonly Entry[] entries = new Entry[PrideData.PrideCount];

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
            }

            public override void Update() {
                base.Update();

                Visible = alpha > 0;

                alpha = Calc.Approach(alpha, shown ? 1 : 0, Engine.DeltaTime * 4);

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
                }
            }

            public void Show() {
                shown = true;
                index = (int)PrideModModuleSettings.GetGlobalPride();
                y = TargetY;
                menu.Focused = false;
            }

            public void Hide() {
                shown = false;
                menu.Focused = true;
            }

            private void SelectedDifferentValue(bool down) {
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

            internal void Display() {
                Vector2 mid = new(Engine.Width * 0.5f, Engine.Height * 0.5f);

                Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * Ease.CubeOut(alpha) * 0.95f);

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
                                    Util.MultiColorLerp(Entity.Scene.TimeActive * 10f, defaultHighlightColors) :
                                    Util.MultiColorPingPong(Entity.Scene.TimeActive * 2f, PrideData.PrideColors[entry.Pride]);

                            MTexture dot = GFX.Gui["dot"];
                            dot.DrawCentered(pos - new Vector2(halfMaxWidth, 0), color * alpha);
                            dot.DrawCentered(pos + new Vector2(halfMaxWidth, 0), color * alpha);
                        }

                        ActiveFont.Draw(entry.Name, pos, new(0.5f, 0.5f), Vector2.One, color * alpha);
                    }
                }
            }
        }

        private GlobalPrideSettingComponent component;

        private bool Opened => !Container.Focused && Container.Current == this;

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
            if (component != null)
                component.Display();
        }

        #endregion
    }
}
