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
		public static string GetHeartSpriteID(this PrideTypes prideType, string originalID)
			=> prideType == PrideTypes.Default ?
				originalID :
				$"PrideMod_crystalheart_{prideType.ToString().ToLower()}";
    }
}
