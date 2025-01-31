namespace Assets.Scripts.Infrastructure.GameSatateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
    }
}
