using System;
using System.Collections.Generic;

namespace _ParticleLife.Core.Runtime {
    [Serializable] public struct Bounds {
        public float xMin,
        xMax,
        yMin,
        yMax,
        zMin,
        zMax;
    }

    public class SharedData {
        public Bounds Bounds;
        public List<int> Particles;
        public List<ParticleView> ParticleViews;

        public SharedData(Bounds _bounds) {
            Bounds = _bounds;
            Particles = new List<int>();
            ParticleViews = new List<ParticleView>();
        }
    }
}