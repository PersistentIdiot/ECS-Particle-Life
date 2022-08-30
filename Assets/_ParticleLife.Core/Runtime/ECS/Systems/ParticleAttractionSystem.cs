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
            // Setup pools
            var velocityPool = _world.GetPool<ECSVelocity>();
            var transformPool = _world.GetPool<ECSTransform>();

            // Iterate through all particles, applying a change in velocity equal to
            // 1/Distance^2 x ForceFromColor x directionToOtherParticle
            for (int i = 0; i < _sharedData.Particles.Count; i++){
                ref ECSVelocity thisVelocity = ref velocityPool.Get(_sharedData.Particles[i]);
                ref ECSTransform thisTransform = ref transformPool.Get(_sharedData.Particles[i]);
                for (int j = 0; j < _sharedData.Particles.Count; j++){
                    ref ECSTransform otherTransform = ref transformPool.Get(_sharedData.Particles[j]);
                    Vector3 direction = otherTransform.Position - thisTransform.Position;
                    float distance = direction.magnitude;
                    float colorForce = _sharedData.ParticleColors.Forces[i][j];
                    thisVelocity.Velocity += (1 / distance * distance) * colorForce * direction * Time.deltaTime;
                }

                thisVelocity.Velocity = Vector3.one;
            }
        }
    }
}