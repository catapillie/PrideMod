using Celeste.Mod.CollabUtils2.Entities;
using Monocle;
using MonoMod.Utils;

namespace Celeste.Mod.PrideMod.Components {
    public class MiniHeartParticleChanger : Component {
        private readonly DynData<AbstractMiniHeart> data;
        private readonly ParticleType[] particles;

        public MiniHeartParticleChanger(DynData<AbstractMiniHeart> heartData, ParticleType[] particleTypes)
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
