using Leopotam.EcsLite;

namespace _ParticleLife.Core.Runtime {
    // Ensures particles stay within the bounds, either by wrapping or bouncing off the "walls"
    public class ParticleBoundsSystem  : IEcsRunSystem {
        public void Run(EcsSystems systems) {
        }
    }
}