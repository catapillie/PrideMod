namespace Celeste.Mod.PrideMod {
    public enum PrideTypes {
		Default,
		Agender,
		Aromantic,
		Asexual,
		Bigender,
		Bisexual,
		Demiboy,
		Demigirl,
		Deminonbinary,
		Demisexual,
		Gay,
		Genderfluid,
		Genderqueer,
		Intersex,
		Lesbian,
		NonBinary,
		Omnisexual,
		Pansexual,
		Plural,
		Polyamorous,
		Polysexual,
		Transgender,
	}

	public static class PrideTypesInfo {
		public static string GetCustomSpriteID(this PrideTypes prideType, string spriteType, string originalID)
			=> prideType == PrideTypes.Default ?
				originalID :
				$"PrideMod_{spriteType}_{prideType.ToString().ToLower()}";
	}
}
