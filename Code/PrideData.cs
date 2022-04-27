using Microsoft.Xna.Framework;
using Monocle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Celeste.Mod.PrideMod {
    public static class PrideData {
		public static readonly int PrideCount = Enum.GetNames(typeof(Pride)).Length;

		private static readonly ReadOnlyDictionary<Pride, Color[]> prideColors = new(new Dictionary<Pride, Color[]> {
			{ Pride.Default, null },
			{ Pride.Agender, new[] {
				Calc.HexToColor("3f3f3f"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("c7ff9a"),
			} },
			{ Pride.Aromantic, new[] {
				Calc.HexToColor("4dcc53"),
				Calc.HexToColor("c7ff9a"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("3f3f3f"),
			} },
			{ Pride.Asexual, new[] {
				Calc.HexToColor("3f3f3f"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("b900b9"),
			} },
			{ Pride.Bigender, new[] {
				Calc.HexToColor("e5a9c8"),
				Calc.HexToColor("fbc4e1"),
				Calc.HexToColor("e3cfff"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("b1deff"),
				Calc.HexToColor("829df5"),
			} },
			{ Pride.Bisexual, new[] {
				Calc.HexToColor("ff0099"),
				Calc.HexToColor("da48ff"),
				Calc.HexToColor("2c7ffb"),
			} },
			{ Pride.Demiboy, new[] {
				Calc.HexToColor("706f6f"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("9ad6ff"),
				Calc.HexToColor("f0f0f0"),
			} },
			{ Pride.Demigirl, new[] {
				Calc.HexToColor("706f6f"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("ffabff"),
				Calc.HexToColor("f0f0f0"),
			} },
			{ Pride.Deminonbinary, new[] {
				Calc.HexToColor("706f6f"),
				Calc.HexToColor("969696"),
				Calc.HexToColor("fff5a1"),
				Calc.HexToColor("f0f0f0"),
			} },
			{ Pride.Demisexual, new[] {
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("3f3f3f"),
				Calc.HexToColor("790c7d"),
				Calc.HexToColor("dddddd"),
			} },
			{ Pride.Gay, new[] {
				Calc.HexToColor("ff2457"),
				Calc.HexToColor("ff8524"),
				Calc.HexToColor("ffc824"),
				Calc.HexToColor("88ff24"),
				Calc.HexToColor("24bbff"),
				Calc.HexToColor("b824ff"),
			} },
			{ Pride.Genderfluid, new[] {
				Calc.HexToColor("ff84ba"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("e045f5"),
				Calc.HexToColor("3f3f3f"),
				Calc.HexToColor("4e5be8"),
			} },
			{ Pride.Genderqueer, new[] {
				Calc.HexToColor("d092fd"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("6ebb37"),
			} },
			{ Pride.Intersex, new[] {
				Calc.HexToColor("f4e742"),
				Calc.HexToColor("ab00f1"),
			} },
			{ Pride.Lesbian, new[] {
				Calc.HexToColor("d62e02"),
				Calc.HexToColor("ff9b58"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("d161a2"),
				Calc.HexToColor("ca0077"),
			} },
			{ Pride.NonBinary, new[] {
				Calc.HexToColor("fef333"),
				Calc.HexToColor("f0f0f0"),
				Calc.HexToColor("9b59d0"),
				Calc.HexToColor("3f3f3f"),
			} },
			{ Pride.Omnisexual, new[] {
				Calc.HexToColor("ffbdde"),
				Calc.HexToColor("ff9fdb"),
				Calc.HexToColor("532679"),
				Calc.HexToColor("9d98ff"),
				Calc.HexToColor("bdccff"),
			} },
			{ Pride.Pansexual, new[] {
				Calc.HexToColor("ff00aa"),
				Calc.HexToColor("ffe100"),
				Calc.HexToColor("00c9ff"),
			} },
			{ Pride.Plural, new[] {
				Calc.HexToColor("ff00b4"),
				Calc.HexToColor("f8e423"),
				Calc.HexToColor("63ff48"),
				Calc.HexToColor("154fa5"),
			} },
			{ Pride.Polyamorous, new[] {
				Calc.HexToColor("2446de"),
				Calc.HexToColor("f8e423"),
				Calc.HexToColor("dc2d2d"),
				Calc.HexToColor("3f3f3f"),
			} },
			{ Pride.Polysexual, new[] {
				Calc.HexToColor("ff4bcf"),
				Calc.HexToColor("13ff86"),
				Calc.HexToColor("3eaaff"),
			} },
			{ Pride.Transgender, new[] {
				Calc.HexToColor("67d1f8"),
				Calc.HexToColor("ffa2b7"),
				Calc.HexToColor("f0f0f0"),
			} },
		});

		public static ReadOnlyDictionary<Pride, ParticleType[]> PrideParticles_HeartGem_P_AnyShine { get; private set; } = null;
		public static ReadOnlyDictionary<Pride, ParticleType[]> PrideParticles_HeartGem_P_FakeShine { get; private set; } = null;
		public static ReadOnlyDictionary<Pride, ParticleType[]> PrideParticles_Cassette_P_Shine { get; private set; } = null;

		public static string GetFormattedName(this Pride pride)
			=> pride switch {
				Pride.Agender => "Agender",
				Pride.Aromantic => "Aromantic",
				Pride.Asexual => "Asexual",
				Pride.Bigender => "Bigender",
				Pride.Bisexual => "Bisexual",
				Pride.Demiboy => "Demiboy",
				Pride.Demigirl => "Demigirl",
				Pride.Deminonbinary => "Deminonbinary",
				Pride.Demisexual => "Demisexual",
				Pride.Gay => "Gay",
				Pride.Genderfluid => "Genderfluid",
				Pride.Genderqueer => "Genderqueer",
				Pride.Intersex => "Intersex",
				Pride.Lesbian => "Lesbian",
				Pride.NonBinary => "Non-Binary",
				Pride.Omnisexual => "Omnisexual",
				Pride.Pansexual => "Pansexual",
				Pride.Plural => "Plural",
				Pride.Polyamorous => "Polyamorous",
				Pride.Polysexual => "Polysexual",
				Pride.Transgender => "Transgender",
				_ => "Default",
			};

		public static string GetCustomTexturePath(this Pride pride, string spriteType, string frame, string originalID)
			=> pride == Pride.Default ?
				originalID :
				$"PrideMod/{spriteType}/{pride.ToString().ToLower()}/{frame}";

		public static string GetCustomTexturePath(this Pride pride, string spriteType, string frame)
			=> $"PrideMod/{spriteType}/{pride.ToString().ToLower()}/{frame}";

		public static string GetCustomSpriteID(this Pride pride, string spriteType, string originalID)
			=> pride == Pride.Default ?
				originalID :
				$"PrideMod_{spriteType}_{pride.ToString().ToLower()}";

		public static string GetCustomSpriteID(this Pride pride, string spriteType)
			=> $"PrideMod_{spriteType}_{pride.ToString().ToLower()}";

		public static Sprite GetCustomSprite(this Pride pride, string spriteType, Sprite originalSprite)
			=> pride == Pride.Default ?
				originalSprite :
				GFX.SpriteBank.Create($"PrideMod_{spriteType}_{pride.ToString().ToLower()}");

		public static Sprite GetCustomSprite(this Pride pride, string spriteType)
			=> GFX.SpriteBank.Create($"PrideMod_{spriteType}_{pride.ToString().ToLower()}");

		public static string GetCustomSummitFlagDecalPath(this Pride pride, string originalPath)
			=> pride == Pride.Default ?
				originalPath :
				$"PrideMod/summitflag/{pride.ToString().ToLower()}/SummitFlag";

		public static string GetCustomFinalFlagDecalPath(this Pride pride, string originalPath)
			=> pride == Pride.Default ?
				originalPath :
				$"PrideMod/finalflag/{pride.ToString().ToLower()}/finalflag";

		public static Color[] GetColors(this Pride pride) => prideColors[pride];

		internal static void InitializeContent() {
			PrideParticles_HeartGem_P_AnyShine	= BuildParticleTypes(HeartGem.P_BlueShine, (p, color) => p.Color = color);
			PrideParticles_HeartGem_P_FakeShine	= BuildParticleTypes(HeartGem.P_FakeShine, (p, color) => p.Color = color);

			PrideParticles_Cassette_P_Shine		= BuildParticleTypes(Cassette.P_Shine, (p, color) => {
				p.Color = color;
				p.Color2 = Color.Lerp(color, Color.White, 0.5f);
			});
		}

		private static ReadOnlyDictionary<Pride, ParticleType[]> BuildParticleTypes(ParticleType from, Action<ParticleType, Color> particleTypeModifier) {
			Dictionary<Pride, ParticleType[]> prideParticleTypes = new();

			foreach (var kv in prideColors) {
				if (kv.Key != Pride.Default) {
					ParticleType[] particleTypes = new ParticleType[kv.Value.Length];
					for (int i = 0; i < particleTypes.Length; i++)
						particleTypeModifier(particleTypes[i] = new ParticleType(from), kv.Value[i]);

					prideParticleTypes[kv.Key] = particleTypes;
				}
			}

			return new(prideParticleTypes);
		}
	}
}
