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
            var particlePool = _world.GetPool<ECSParticle>();

            // Iterate through all particles, applying a change in velocity equal to
            // 1/Distance^2 x ForceFromColor x directionToOtherParticle
            for (int i = 0; i < _sharedData.Particles.Count; i++){
                //Get thisParticle references
                ref ECSTransform thisTransform = ref transformPool.Get(_sharedData.Particles[i]);
                ref ECSParticle thisParticle = ref particlePool.Get(_sharedData.Particles[i]);
                ref ECSVelocity thisVelocity = ref velocityPool.Get(_sharedData.Particles[i]);
                for (int j = 0; j < _sharedData.Particles.Count; j++){
                    if (i == j) continue;
                    //Debug.Log($"{nameof(ParticleAttractionSystem)}.{nameof(Run)}() - i,j: ({i},{j})");
                    //Get otherParticle references
                    ref ECSTransform otherTransform = ref transformPool.Get(_sharedData.Particles[j]);
                    ref ECSParticle otherParticle = ref particlePool.Get(_sharedData.Particles[j]);

                    //Setup for calculation
                    Vector3 direction = otherTransform.Position - thisTransform.Position;
                    float distance = Vector3.Distance(thisTransform.Position, otherTransform.Position);
                    Debug.Assert(distance != 0);

                    float colorForce =
                    _sharedData.ColorForceMultiplier * _sharedData.ParticleColors.Forces[thisParticle.Color][otherParticle.Color];

                    // Calculate and apply velocity change
                    thisVelocity.Velocity += (1 / distance * distance) * colorForce * direction * Time.deltaTime;
                }

                //thisVelocity.Velocity = Vector3.one;
            }
        }
    }
}