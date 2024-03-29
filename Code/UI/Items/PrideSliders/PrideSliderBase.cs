﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste.Mod.PrideMod.UI {
    public class PrideSliderBase : TextMenu.Option<int> {
        public static readonly Color[] DefaultHighlightColors = new[] {
            TextMenu.HighlightColorA,
            TextMenu.HighlightColorB,
        };

        public const int PRIDE_SETTINGS_UI_SCALE = 6;

        public Pride Pride {
            get => (Pride)Index;
            set => Index = (int)value;
        }

        public virtual bool PerformCustomRendering => false;

        public PrideSliderBase(string label, Pride value)
            : base(label) {
            int val = (int)value;
            for (int i = 0; i < PrideData.PrideCount; i++)
                Add(((Pride)i).GetFormattedName(), i, val == i);
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
                HiresRenderer.EndRender();
                HiresRenderer.BeginRender(sampler: SamplerState.PointClamp);

                CustomRender(position);

                HiresRenderer.EndRender();
                HiresRenderer.BeginRender();
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
                Pride pride = slider.Pride;
                self.HighlightColor = pride == Pride.Default ?
                    Util.MultiColorLerp(self.Scene.RawTimeActive * 10f, DefaultHighlightColors) :
                    Util.MultiColorPingPong(self.Scene.RawTimeActive * 2f, pride.GetColors());
            }
        }

        #endregion
    }
}
