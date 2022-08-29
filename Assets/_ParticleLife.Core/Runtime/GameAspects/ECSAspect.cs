using _ParticleLife.Core.Runtime.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _ParticleLife.Core.Runtime.GameAspects {
    public class ECSAspect : MonoBehaviour, IInitGameAspect {
        [Header("Settings")]
        public Bounds bounds;
        [SerializeField] private int numberOfParticles = 10;
        

        [Header("References")]
        [SerializeField] private ParticleView ParticlePrefab;

        public EcsWorld World;
        public EcsSystems Systems;

        private SharedData sharedData;

        public void Init() {
            Debug.Log($"{nameof(ECSAspect)}.{nameof(Init)}()");

            // Setup sharedData
            sharedData = new SharedData(bounds);

            // Setup ECSWorld, add systems but wait to initialize them
            World = new EcsWorld();
            Systems = new EcsSystems(World, shared: sharedData).Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());

            // Spawn particles before init because systems' init will rely on particles existing
            SpawnParticles();

            // Init systems after spawning
            Systems.Init();
        }

        private void SpawnParticles() {
            // Setup pools used for setting up particle components
            EcsPool<ECSTransform> transformPool = World.GetPool<ECSTransform>();
            EcsPool<ECSParticle> particlePool = World.GetPool<ECSParticle>();
            EcsPool<ECSVelocity> velocityPool = World.GetPool<ECSVelocity>();

            for (int i = 0; i < numberOfParticles; i++){
                // Set up random position for particle
                Vector3 randomPosition = new Vector3(
                    Random.Range(sharedData.Bounds.xMin, sharedData.Bounds.xMax),
                    Random.Range(sharedData.Bounds.yMin, sharedData.Bounds.yMax),
                    Random.Range(sharedData.Bounds.zMin, sharedData.Bounds.zMax)
                );

                // Spawn Entity
                int entity = World.NewEntity();
                sharedData.Particles.Add(entity);
                
                // Setup Entity components
                ref ECSTransform ecsTransform = ref transformPool.Add(entity);
                ref ECSParticle particle = ref particlePool.Add(entity);
                ref ECSVelocity ecsVelocity = ref velocityPool.Add(entity);
                ecsVelocity.Velocity = -randomPosition;

                // Spawn and setup view
                var particleView = Instantiate(ParticlePrefab);
                sharedData.ParticleViews.Add(particleView);
                particleView.Entity = entity;
                particleView.transform.position = randomPosition;
                particleView.Rigidbody.velocity = -randomPosition;
            }

        }

        void OnDestroy() {
            // destroy systems logical group.
            if (Systems != null){
                Systems.Destroy();
                Systems = null;
            }

            // destroy world.
            if (World != null){
                World.Destroy();
                World = null;
            }
        }
    }
}