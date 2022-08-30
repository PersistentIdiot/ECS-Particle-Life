using _ParticleLife.Core.Runtime.Components;
using Leopotam.EcsLite;

namespace _ParticleLife.Core.Runtime {
    // Ensures particles stay within the bounds, either by wrapping or bouncing off the "walls"
    public class ParticleBoundsSystem  : IEcsRunSystem , IEcsInitSystem{
        private SharedData _sharedData;
        private EcsWorld _world;
        
        public void Init(EcsSystems systems) {
            _sharedData = systems.GetShared<SharedData>();
            _world = systems.GetWorld();
        }
        
        public void Run(EcsSystems systems) {
            // Setup pools
            var transformPool = _world.GetPool<ECSTransform>();
            var particlePool = _world.GetPool<ECSParticle>();
            for (int i = 0; i < _sharedData.Particles.Count; i++){
                ref ECSTransform transform = ref transformPool.Get(_sharedData.Particles[i]);
                bool exceededBounds = false;
                // Left
                if (transform.Position.x < _sharedData.Bounds.xMin){
                    transform.Position.x = _sharedData.Bounds.xMax;
                    exceededBounds = true;
                }
                
                // Right
                if (transform.Position.x > _sharedData.Bounds.xMax){
                    transform.Position.x = _sharedData.Bounds.xMin;
                    exceededBounds = true;
                }
                
                // Top
                if (transform.Position.y > _sharedData.Bounds.yMax){
                    transform.Position.y = _sharedData.Bounds.yMin;
                    exceededBounds = true;
                }
                
                // Bottom
                if (transform.Position.y < _sharedData.Bounds.yMin){
                    transform.Position.y = _sharedData.Bounds.yMax;
                    exceededBounds = true;
                }
                
                // Front
                if (transform.Position.z > _sharedData.Bounds.zMax){
                    transform.Position.z = _sharedData.Bounds.zMin;
                    exceededBounds = true;
                }
                
                // Back
                if (transform.Position.z < _sharedData.Bounds.zMin){
                    transform.Position.z = _sharedData.Bounds.zMax;
                    exceededBounds = true;
                }


                ref ECSParticle particle = ref particlePool.Get(_sharedData.Particles[i]);
                if (exceededBounds){
                    particle.ParticleView.transform.position = transform.Position;
                }
            }
        }
    }
}