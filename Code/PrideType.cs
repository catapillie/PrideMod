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

		public static string GetFormattedName(this PrideTypes prideType)
			=> prideType switch {
				PrideTypes.Agender => "Agender",
				PrideTypes.Aromantic => "Aromantic",
				PrideTypes.Asexual => "Asexual",
				PrideTypes.Bigender => "Bigender",
				PrideTypes.Bisexual => "Bisexual",
				PrideTypes.Demiboy => "Demiboy",
				PrideTypes.Demigirl => "Demigirl",
				PrideTypes.Deminonbinary => "Deminonbinary",
				PrideTypes.Demisexual => "Demisexual",
				PrideTypes.Gay => "Gay",
				PrideTypes.Genderfluid => "Genderfluid",
				PrideTypes.Genderqueer => "Genderqueer",
				PrideTypes.Intersex => "Intersex",
				PrideTypes.Lesbian => "Lesbian",
				PrideTypes.NonBinary => "Non-Binary",
				PrideTypes.Omnisexual => "Omnisexual",
				PrideTypes.Pansexual => "Pansexual",
				PrideTypes.Plural => "Plural",
				PrideTypes.Polyamorous => "Polyamorous",
				PrideTypes.Polysexual => "Polysexual",
				PrideTypes.Transgender => "Transgender",
				_ => "Default",
			};

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
