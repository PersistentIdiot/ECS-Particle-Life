using System.Collections.Generic;
using UnityEngine;

namespace _ParticleLife.Core.Runtime.GameAspects {
    public class GameManager : Singleton<GameManager> {
        
        [Header("Debug")]
        [SerializeField] private bool debugging = false;
        [SerializeField] private List<IGameAspect> _aspects = new List<IGameAspect>();

        private void Awake() {
            foreach (var aspect in Instance.GetComponents<IGameAspect>()){
                if (debugging) Debug.Log($"{nameof(GameManager)}.{nameof(Awake)}() - Adding aspect: {aspect.GetType()}");
                AddAspect(aspect);
            }
        }

        public static void AddAspect(IGameAspect aspect) {
            Debug.Assert(!Instance._aspects.Contains(aspect), "Added a GameAspect already added before!", Instance);
            Instance._aspects.Add(aspect);
            if (aspect is IInitGameAspect initGameAspect){
                initGameAspect.Init();
            }

        }

        public static bool TryGetAspect<T>(out T aspect) {
            return Instance.TryGetComponent(out aspect);
        }

        public static T GetAspect<T>() {
            return Instance.GetComponent<T>();
        }
    }
}