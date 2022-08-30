using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

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
        public ParticleColors ParticleColors;
        public float AnnealingFactor;
        public float ColorForceMultiplier;

        public SharedData(Bounds _bounds, ParticleColors _particleColors) {
            Bounds = _bounds;
            Particles = new List<int>();
            ParticleViews = new List<ParticleView>();
            ParticleColors = _particleColors;
        }
    }

    [Serializable] public struct ParticleColors {
        public int ColorCount;
        public float[][] Forces;

        public ParticleColors(int colorCount, [NotNull] Action<float[][]> forceInitializer) {
            ColorCount = colorCount;
            Forces = Enumerable
            .Range(0, colorCount)                   // colorCount items
            .Select(i => new float[colorCount])  // each of which is colorCount items array of float
            .ToArray();                             // materialized as array

            forceInitializer.Invoke(Forces);
        }
    }
}