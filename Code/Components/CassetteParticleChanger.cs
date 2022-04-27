using Monocle;

namespace Celeste.Mod.PrideMod.Components {
    public class CassetteParticleChanger : Component {
        private readonly ParticleType[] particles;
        public ParticleType Particle { get; private set; }

        public CassetteParticleChanger(ParticleType[] particleTypes)
            : base(active: true, visible: false) {
            particles = particleTypes;
        }

        public override void Update() {
            base.Update();

            if (Scene.OnInterval(0.1f))
                Particle = Calc.Random.Choose(particles);
        }
    }
}
