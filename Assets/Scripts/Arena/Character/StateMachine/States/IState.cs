namespace Assets.Scripts.Arena.Character.StateMachine.States
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
    }
}
