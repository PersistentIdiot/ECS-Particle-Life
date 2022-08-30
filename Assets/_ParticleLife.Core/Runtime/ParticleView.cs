using System;
using _ParticleLife.Core.Runtime.Components;
using _ParticleLife.Core.Runtime.GameAspects;
using Leopotam.EcsLite;
using UnityEngine;

namespace _ParticleLife.Core.Runtime {
    public class ParticleView : MonoBehaviour {
        public int Entity;
        public Rigidbody Rigidbody;

        private ECSAspect ECSAspect;
        private EcsPool<ECSVelocity> velocityPool;
        private EcsPool<ECSTransform> transformPool;

        private void Awake() {
            ECSAspect = GameManager.GetAspect<ECSAspect>();
            velocityPool = ECSAspect.World.GetPool<ECSVelocity>();
            transformPool = ECSAspect.World.GetPool<ECSTransform>();
        }

        private void LateUpdate() {
            ref ECSVelocity velocityComponent = ref velocityPool.Get(Entity);
            ref ECSTransform transformComponent = ref transformPool.Get(Entity);
            Rigidbody.velocity = velocityComponent.Velocity.normalized;
            transformComponent.Position = transform.position;
        }
    }
}