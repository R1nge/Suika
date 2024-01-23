using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameOverState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly PlayerInput _playerInput;
        private readonly ContinueGameService _continueGameService;

        public GameOverState(GameStateMachine gameStateMachine, UIStateMachine uiStateMachine, PlayerInput playerInput, ContinueGameService continueGameService)
        {
            _gameStateMachine = gameStateMachine;
            _uiStateMachine = uiStateMachine;
            _playerInput = playerInput;
            _continueGameService = continueGameService;
        }

        public async void Enter()
        {
            _playerInput.Disable();
            //Race condition, need to await
            //I don't want to rewrite the state machine
            //even with an IDE and AI it would take some time
            //so will make this the hacky way
            //Don't repeat at home
            _gameStateMachine.SwitchState(GameStateType.SaveData);
            await _uiStateMachine.SwitchStateWithoutExitFromPrevious(UIStateType.GameOver);
            _continueGameService.DeleteContinueData();
        }

        public void Exit()
        {
        }
    }
}