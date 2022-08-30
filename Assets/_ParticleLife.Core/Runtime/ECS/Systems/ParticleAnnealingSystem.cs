using _ParticleLife.Core.Runtime.Components;
using Leopotam.EcsLite;

namespace _ParticleLife.Core.Runtime {
    // Slows down particles
    public class ParticleAnnealingSystem : IEcsRunSystem , IEcsInitSystem{
        private SharedData _sharedData;
        private EcsWorld _world;
        
        public void Init(EcsSystems systems) {
            _sharedData = systems.GetShared<SharedData>();
            _world = systems.GetWorld();
        }
        
        public void Run(EcsSystems systems) {
            // Setup pools
            var velocityPool = _world.GetPool<ECSVelocity>();
            for (int i = 0; i < _sharedData.Particles.Count; i++){
                ref ECSVelocity thisVelocity = ref velocityPool.Get(_sharedData.Particles[i]);
                thisVelocity.Velocity *= _sharedData.AnnealingFactor;
            }
        }
    }
}