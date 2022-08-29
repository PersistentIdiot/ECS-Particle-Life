using UnityEngine;

namespace _ParticleLife.Core.Runtime.GameAspects {
    public class BoundsAspect : MonoBehaviour, IInitGameAspect {
        [Header("References")]
        [SerializeField] private ECSAspect EcsAspect;
        [SerializeField] private GameObject FrontWall;
        [SerializeField] private GameObject BackWall;
        [SerializeField] private GameObject TopWall;
        [SerializeField] private GameObject BottomWall;
        [SerializeField] private GameObject LeftWall;
        [SerializeField] private GameObject RightWall;

        private Bounds bounds;

        public void Init() {
            Debug.Log($"{nameof(BoundsAspect)}.{nameof(Init)}() - Message");
            bounds = EcsAspect.bounds;
            // Front Wall
            var pos = FrontWall.transform.position;
            pos.z = bounds.zMax;
            FrontWall.transform.position = pos;
            FrontWall.transform.localScale = new Vector3(bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMin, bounds.zMax - bounds.zMin) / 8;

            // Back Wall
            pos = BackWall.transform.position;
            pos.z = bounds.zMin;
            BackWall.transform.position = pos;
            BackWall.transform.localScale = new Vector3(bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMin, bounds.zMax - bounds.zMin) / 8;

            // Left Wall
            pos = LeftWall.transform.position;
            pos.x = bounds.xMin;
            LeftWall.transform.position = pos;
            LeftWall.transform.localScale = new Vector3(bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMin, bounds.zMax - bounds.zMin) / 8;

            // Right Wall
            pos = RightWall.transform.position;
            pos.x = bounds.xMax;
            RightWall.transform.position = pos;
            RightWall.transform.localScale = new Vector3(bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMin, bounds.zMax - bounds.zMin) / 8;

            // Top Wall
            pos = TopWall.transform.position;
            pos.y = bounds.yMax;
            TopWall.transform.position = pos;
            TopWall.transform.localScale = new Vector3(bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMin, bounds.zMax - bounds.zMin) / 8;

            // Bottom Wall
            pos = BottomWall.transform.position;
            pos.y = bounds.yMin;
            BottomWall.transform.position = pos;
            BottomWall.transform.localScale = new Vector3(bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMin, bounds.zMax - bounds.zMin) / 8;
        }
    }
}