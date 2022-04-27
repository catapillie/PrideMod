using Microsoft.Xna.Framework;
using System;

namespace Celeste.Mod.PrideMod {
    public static class Util {
		public static float Mod(float x, float y) {
			float r = x % y;
			return r < 0 ? r + y : r;
		}

		public static int Mod(int x, int y) {
			int r = x % y;
			return r < 0 ? r + y : r;
		}

		/// <summary>
		/// Calculates a triangle wave given input, min and max values.
		/// </summary>
		/// <param name="x">Triangle wave function input.</param>
		/// <param name="min">The minimum value of the triangle wave.</param>
		/// <param name="max">The maximum value of the triangle wave.</param>
		/// <returns>The value of a triangle wave going from min and max values, at x.</returns>
		public static float Triangle(float x, float min, float max) {
			float d = max - min;
			return Math.Abs(Mod(x, 2 * d) - d) + min;
		}

		/// <summary>
		/// Calculates a triangle wave given an input.
		/// </summary>
		/// <param name="x">Triangle wave function input.</param>
		/// <returns>The value of a triangle wave between 0 and 1, at x.</returns>
		public static float Triangle(float x)
			=> Math.Abs(Mod(x, 2) - 1);
		
		public static Color MultiColorLerp(float at, params Color[] colors) {
			if (colors.Length == 0)
				return default;

			float m = Mod(at, colors.Length);
			int fromIndex = (int)Math.Floor(m);
			int toIndex = Mod(fromIndex + 1, colors.Length);

			return Color.Lerp(colors[fromIndex], colors[toIndex], m - fromIndex);
		}

		public static Color MultiColorPingPong(float at, params Color[] colors)
			=> MultiColorLerp(Triangle(at) * (colors.Length - 1), colors);
	}
}
