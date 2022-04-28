using Microsoft.Xna.Framework;
using Monocle;
using System;

namespace Celeste.Mod.PrideMod.UI {
    public class ConfettiPrideSlider : PrideSliderBase {
        public override bool PerformCustomRendering => Container.Current == this && ready;

        public struct Particle {
			public Vector2 Position, Speed;
			public Color Color;
			public float Timer, Percent, Duration, Alpha, Approach;
		}

		private static MTexture[] confettiTextures;
		private static readonly Color[] defaultConfettiColors = new Color[] {
			Calc.HexToColor("fe2074"),
			Calc.HexToColor("205efe"),
			Calc.HexToColor("cefe20"),
		};

		private readonly Particle[] particles = new Particle[30];

		private readonly float yOffset;
		private bool ready;

		public ConfettiPrideSlider(string label, Pride value, float yOffset)
			: base(label, value) {
			this.yOffset = yOffset;

			OnEnter = SpawnConfetti;
			OnLeave = () => ready = false;
		}

		public override void SelectedDifferentValue() => SpawnConfetti();
		public override void ConfirmPressed() => SpawnConfetti();

		private void SpawnConfetti() {
			var menu = Container;	
			var h = Height() * 0.5f;
			var wiggle = SelectWiggler.Value * 8f;
			Vector2 position = new(
				-menu.Justify.X * menu.Width + menu.Width + 100,
				-menu.Justify.Y * menu.Height + h + wiggle + yOffset + 150
			);

			for (int i = 0; i < particles.Length; i++) {
				particles[i].Position = position + new Vector2(Calc.Random.Range(-3, 3), Calc.Random.Range(-3, 3)) * PRIDE_SETTINGS_UI_SCALE;
				particles[i].Color = Calc.Random.Choose(Pride == Pride.Default ? defaultConfettiColors : Pride.GetColors());
				particles[i].Timer = Calc.Random.NextFloat();
				particles[i].Duration = Calc.Random.NextFloat(2f) + 2f;
				particles[i].Percent = 0f;
				particles[i].Alpha = 1f;
				float angleRadians = -(float)Math.PI / 2f + Calc.Random.Range(-0.5f, 0.5f);
				int rotation = Calc.Random.Range(140, 220);
				particles[i].Speed = Calc.AngleToVector(angleRadians, rotation) * 1.2f;
			}

			ready = true;

			if (Engine.Scene is not Level)
				Audio.Play(SFX.game_07_checkpointconfetti);
		}

		public override void Update() {
			base.Update();

			if (ready) {
				for (int i = 0; i < particles.Length; i++) {
					particles[i].Position += particles[i].Speed * PRIDE_SETTINGS_UI_SCALE * Engine.DeltaTime;
					particles[i].Speed.X = Calc.Approach(particles[i].Speed.X, 0f, 80f * Engine.DeltaTime);
					particles[i].Speed.Y = Calc.Approach(particles[i].Speed.Y, 20f, 500f * Engine.DeltaTime);
					particles[i].Timer += Engine.DeltaTime;
					particles[i].Percent += Engine.DeltaTime / particles[i].Duration;
					particles[i].Alpha = Calc.ClampedMap(particles[i].Percent, 0.9f, 1f, 1f, 0f);
					if (particles[i].Speed.Y > 0f)
						particles[i].Approach = Calc.Approach(particles[i].Approach, 5f, Engine.DeltaTime * 16f);
				}
			}
		}

        public override void CustomRender(Vector2 position) {
            base.CustomRender(position);

			foreach (Particle p in particles) {
				Vector2 particlePos = p.Position + Container.Position;
				float rotation;
                if (p.Speed.Y < 0f) {
                    rotation = p.Speed.Angle();
                } else {
                    rotation = (float)Math.Sin(p.Timer * 4f) * 1f;
                    particlePos += Calc.AngleToVector((float)Math.PI / 2f + rotation, p.Approach);
				}

				particlePos = Calc.Round(particlePos / PRIDE_SETTINGS_UI_SCALE) * PRIDE_SETTINGS_UI_SCALE;
				MTexture tex = confettiTextures[Util.Mod((int)Math.Round(rotation / MathHelper.TwoPi * 23f), 23)];
				tex.DrawCentered(particlePos, p.Color * p.Alpha, PRIDE_SETTINGS_UI_SCALE);
			}
		}

		internal static void InitializeContent() {
			confettiTextures = GFX.Gui.GetAtlasSubtextures("PrideMod/pixelatedConfetti/").ToArray();
        }
	}
}
