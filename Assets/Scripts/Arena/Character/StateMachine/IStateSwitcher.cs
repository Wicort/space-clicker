using Assets.Scripts.Arena.Character.StateMachine.States;

namespace Assets.Scripts.Arena.Character.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
    }
}
