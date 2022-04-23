using Monocle;

namespace Celeste.Mod.PrideMod {
    public static class PrideModSpriteBanks {
        public static SpriteBank AgenderSpriteBank			{ get; private set; }
		public static SpriteBank AromanticSpriteBank		{ get; private set; }
		public static SpriteBank AsexualSpriteBank			{ get; private set; }
		public static SpriteBank BigenderSpriteBank			{ get; private set; }
		public static SpriteBank BisexualSpriteBank			{ get; private set; }
		public static SpriteBank DemiboySpriteBank			{ get; private set; }
		public static SpriteBank DemigirlSpriteBank			{ get; private set; }
		public static SpriteBank DeminonbinarySpriteBank	{ get; private set; }
		public static SpriteBank DemisexualSpriteBank		{ get; private set; }
		public static SpriteBank GaySpriteBank				{ get; private set; }
		public static SpriteBank GenderfluidSpriteBank		{ get; private set; }
		public static SpriteBank GenderqueerSpriteBank		{ get; private set; }
		public static SpriteBank IntersexSpriteBank			{ get; private set; }
		public static SpriteBank LesbianSpriteBank			{ get; private set; }
		public static SpriteBank NonBinarySpriteBank		{ get; private set; }
		public static SpriteBank OmnisexualSpriteBank		{ get; private set; }
		public static SpriteBank PansexualSpriteBank		{ get; private set; }
		public static SpriteBank PluralSpriteBank			{ get; private set; }
		public static SpriteBank PolyamorousSpriteBank		{ get; private set; }
		public static SpriteBank PolysexualSpriteBank		{ get; private set; }
		public static SpriteBank TransgenderSpriteBank		{ get; private set; }

        internal static void Load() {
            AgenderSpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/AgenderSprites.xml");
			AromanticSpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/AromanticSprites.xml");		 
			AsexualSpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/AsexualSprites.xml");		 
			BigenderSpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/BigenderSprites.xml");		 
			BisexualSpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/BisexualSprites.xml");		 
			DemiboySpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/DemiboySprites.xml");		 
			DemigirlSpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/DemigirlSprites.xml");		 
			DeminonbinarySpriteBank = new SpriteBank(GFX.Game, "Graphics/PrideMod/DeminonbinarySprites.xml");	 
			DemisexualSpriteBank	= new SpriteBank(GFX.Game, "Graphics/PrideMod/DemisexualSprites.xml");	 
			GaySpriteBank			= new SpriteBank(GFX.Game, "Graphics/PrideMod/GaySprites.xml");			 
			GenderfluidSpriteBank	= new SpriteBank(GFX.Game, "Graphics/PrideMod/GenderfluidSprites.xml");	 
			GenderqueerSpriteBank	= new SpriteBank(GFX.Game, "Graphics/PrideMod/GenderqueerSprites.xml");	 
			IntersexSpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/IntersexSprites.xml");		 
			LesbianSpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/LesbianSprites.xml");		 
			NonBinarySpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/NonBinarySprites.xml");		 
			OmnisexualSpriteBank	= new SpriteBank(GFX.Game, "Graphics/PrideMod/OmnisexualSprites.xml");	 
			PansexualSpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/PansexualSprites.xml");		 
			PluralSpriteBank		= new SpriteBank(GFX.Game, "Graphics/PrideMod/PluralSprites.xml");		 
			PolyamorousSpriteBank	= new SpriteBank(GFX.Game, "Graphics/PrideMod/PolyamorousSprites.xml");	 
			PolysexualSpriteBank	= new SpriteBank(GFX.Game, "Graphics/PrideMod/PolysexualSprites.xml");
			TransgenderSpriteBank	= new SpriteBank(GFX.Game, "Graphics/PrideMod/TransgenderSprites.xml");
	}
    }
}
