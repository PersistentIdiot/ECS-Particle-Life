using _ParticleLife.Core.Runtime.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _ParticleLife.Core.Runtime {
    // Handles attraction/repulsion of particles based on "color"
    public class ParticleAttractionSystem : IEcsRunSystem, IEcsInitSystem {
        private SharedData _sharedData;
        private EcsWorld _world;

        public void Init(EcsSystems systems) {
            _sharedData = systems.GetShared<SharedData>();
            _world = systems.GetWorld();
        }

        public void Run(EcsSystems systems) {
            var pool = _world.GetPool<ECSVelocity>();
            for (int i = 0; i < _sharedData.Particles.Count; i++){
                ref ECSVelocity ecsVelocity = ref pool.Get(_sharedData.Particles[i]);
                ecsVelocity.Velocity = Vector3.one;
            }
        }
    }
}