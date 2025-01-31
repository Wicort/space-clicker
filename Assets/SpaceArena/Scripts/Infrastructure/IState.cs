namespace Assets.Scripts.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
