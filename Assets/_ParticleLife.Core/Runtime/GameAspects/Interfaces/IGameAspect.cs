namespace _ParticleLife.Core.Runtime.GameAspects {
    public interface IGameAspect {}

    public interface IInitGameAspect : IGameAspect {
        public void Init();
    }
}