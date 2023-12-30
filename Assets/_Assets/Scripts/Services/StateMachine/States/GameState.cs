namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;

        public GameState(GameStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Enter()
        {
            //TODO: show in game ui
            //TODO: spawn player
        }

        public void Exit() { }
    }
}