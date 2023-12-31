namespace _Assets.Scripts.Services.StateMachine.States
{
    //TODO: two states: ResetAndRetry and ResetAndMainMenu
    public class ResetAndRetry : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ResetService _resetService;

        public ResetAndRetry(GameStateMachine gameStateMachine, ResetService resetService)
        {
            _gameStateMachine = gameStateMachine;
            _resetService = resetService;
        }

        public void Enter()
        {
            _resetService.Reset();
            _gameStateMachine.SwitchState(GameStateType.Game);
        }

        public void Exit()
        {
        }
    }
}