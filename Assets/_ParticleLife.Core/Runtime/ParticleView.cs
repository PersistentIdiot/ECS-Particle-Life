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

        private void Awake() {
            ECSAspect = GameManager.GetAspect<ECSAspect>();
            velocityPool = ECSAspect.World.GetPool<ECSVelocity>();
        }

        private void Update() {
            ref ECSVelocity velocityComponent = ref velocityPool.Get(Entity);
            Rigidbody.velocity = velocityComponent.Velocity;
        }
    }
}