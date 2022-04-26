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
	}
}
