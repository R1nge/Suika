namespace _Assets.Scripts.Services.UIs.StateMachine
{
    // TODO: await for enter and exit tasks?
    public interface IUIState
    {
        void Enter();
        void Exit();
    }
}