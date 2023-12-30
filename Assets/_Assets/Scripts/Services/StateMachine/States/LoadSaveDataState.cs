namespace _Assets.Scripts.Services.StateMachine.States
{
    public class LoadSaveDataState : IGameState
    {
        private readonly GameStateMachine _stateMachine;

        public LoadSaveDataState(GameStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }
    }
}