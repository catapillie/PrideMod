using Monocle;
using MonoMod.Utils;

namespace Celeste.Mod.PrideMod.Components {
    public class CrystalHeartParticleChanger : Component {
        private readonly DynData<HeartGem> data;
        private readonly ParticleType[] particles;

        public CrystalHeartParticleChanger(DynData<HeartGem> heartData, ParticleType[] particleTypes)
            : base(active: true, visible: false) {
            data = heartData;
            particles = particleTypes;
        }

        public override void Update() {
            base.Update();

            if (Scene.OnInterval(0.1f))
                data["shineParticle"] = Calc.Random.Choose(particles);
        }
    }
}
