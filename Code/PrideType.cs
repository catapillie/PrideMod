using System;

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

	public static class PrideData {
		public static readonly int PrideCount = Enum.GetNames(typeof(PrideTypes)).Length;

		public static string GetCustomSpriteID(this PrideTypes prideType, string spriteType, string originalID)
			=> prideType == PrideTypes.Default ?
				originalID :
				$"PrideMod_{spriteType}_{prideType.ToString().ToLower()}";

		public static string GetCustomSummitFlagDecalPath(this PrideTypes prideType, string originalPath)
			=> prideType == PrideTypes.Default ?
				originalPath :
				$"PrideMod/summitflag/{prideType.ToString().ToLower()}/SummitFlag";

		public static string GetCustomFinalFlagDecalPath(this PrideTypes prideType, string originalPath)
			=> prideType == PrideTypes.Default ?
				originalPath :
				$"PrideMod/finalflag/{prideType.ToString().ToLower()}/finalflag";
	}
}
