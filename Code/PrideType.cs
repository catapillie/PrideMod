using Microsoft.Xna.Framework;
using Monocle;
using System;
using System.Collections.Generic;

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

		public static readonly Dictionary<PrideTypes, Color[]> PrideColors = new() {
			{ PrideTypes.Default,
				null
			},

			{ PrideTypes.Agender, new[] {
				Calc.HexToColor("3f3f3f"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("c7ff9a"),
			} },

			{ PrideTypes.Aromantic, new[] {
				Calc.HexToColor("4dcc53"),
				Calc.HexToColor("c7ff9a"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("3f3f3f"),
			} },

			{ PrideTypes.Asexual, new[] {
				Calc.HexToColor("3f3f3f"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("b900b9"),
			} },
			{ PrideTypes.Bigender, new[] {
				Calc.HexToColor("e5a9c8"),
				Calc.HexToColor("fbc4e1"),
				Calc.HexToColor("e3cfff"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("b1deff"),
				Calc.HexToColor("829df5"),
			} },
			{ PrideTypes.Bisexual, new[] {
				Calc.HexToColor("ff0099"),
				Calc.HexToColor("da48ff"),
				Calc.HexToColor("2c7ffb"),
			} },
			{ PrideTypes.Demiboy, new[] {
				Calc.HexToColor("706f6f"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("9ad6ff"),
				Calc.HexToColor("f0f0f0"),
			} },
			{ PrideTypes.Demigirl, new[] {
				Calc.HexToColor("706f6f"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("ffabff"),
				Calc.HexToColor("f0f0f0"),
			} },
			{ PrideTypes.Deminonbinary, new[] {
				Calc.HexToColor("706f6f"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("fff5a1"),
				Calc.HexToColor("f0f0f0"),
			} },
			{ PrideTypes.Demisexual, new[] {
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("3f3f3f"),
				Calc.HexToColor("790c7d"),
				Calc.HexToColor("dddddd"),
			} },
			{ PrideTypes.Gay, new[] {
				Calc.HexToColor("ff2457"),
				Calc.HexToColor("ff8524"),
				Calc.HexToColor("ffc824"),
				Calc.HexToColor("88ff24"),
				Calc.HexToColor("24bbff"),
				Calc.HexToColor("b824ff"),
			} },
			{ PrideTypes.Genderfluid, new[] {
				Calc.HexToColor("ff84ba"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("e045f5"),
				Calc.HexToColor("3f3f3f"),
				Calc.HexToColor("4e5be8"),
			} },
			{ PrideTypes.Genderqueer, new[] {
				Calc.HexToColor("d092fd"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("6ebb37"),
			} },
			{ PrideTypes.Intersex, new[] {
				Calc.HexToColor("f4e742"),
				Calc.HexToColor("ab00f1"),
			} },
			{ PrideTypes.Lesbian, new[] {
				Calc.HexToColor("d62e02"),
				Calc.HexToColor("ff9b58"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("d161a2"),
				Calc.HexToColor("ca0077"),
			} },
			{ PrideTypes.NonBinary, new[] {
				Calc.HexToColor("fef333"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("9b59d0"),
				Calc.HexToColor("3f3f3f"),
			} },
			{ PrideTypes.Omnisexual, new[] {
				Calc.HexToColor("ffbdde"),
				Calc.HexToColor("ff9fdb"),
				Calc.HexToColor("532679"),
				Calc.HexToColor("9d98ff"),
				Calc.HexToColor("bdccff"),
			} },
			{ PrideTypes.Pansexual, new[] {
				Calc.HexToColor("ff00aa"),
				Calc.HexToColor("ffe100"),
				Calc.HexToColor("00c9ff"),
			} },
			{ PrideTypes.Plural, new[] {
				Calc.HexToColor("ff00b4"),
				Calc.HexToColor("f8e423"),
				Calc.HexToColor("63ff48"),
				Calc.HexToColor("154fa5"),
			} },
			{ PrideTypes.Polyamorous, new[] {
				Calc.HexToColor("2446de"),
				Calc.HexToColor("f8e423"),
				Calc.HexToColor("dc2d2d"),
				Calc.HexToColor("3f3f3f"),
			} },
			{ PrideTypes.Polysexual, new[] {
				Calc.HexToColor("ff4bcf"),
				Calc.HexToColor("13ff86"),
				Calc.HexToColor("3eaaff"),
			} },
			{ PrideTypes.Transgender, new[] {
				Calc.HexToColor("67d1f8"),
				Calc.HexToColor("ffa2b7"),
				Calc.HexToColor("f0f0f0"),
			} },
		};

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

		public static string GetCustomTexturePath(this PrideTypes prideType, string spriteType, string frame, string originalID)
			=> prideType == PrideTypes.Default ?
				originalID :
				$"PrideMod/{spriteType}/{prideType.ToString().ToLower()}/{frame}";

		public static string GetCustomSpriteID(this PrideTypes prideType, string spriteType, string originalID)
			=> prideType == PrideTypes.Default ?
				originalID :
				$"PrideMod_{spriteType}_{prideType.ToString().ToLower()}";

		public static Sprite GetCustomSprite(this PrideTypes prideType, string spriteType, Sprite originalSprite)
			=> prideType == PrideTypes.Default ?
				originalSprite :
				GFX.SpriteBank.Create($"PrideMod_{spriteType}_{prideType.ToString().ToLower()}");

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
