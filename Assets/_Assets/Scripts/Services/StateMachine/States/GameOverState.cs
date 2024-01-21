using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

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

        public void Enter()
        {
            _playerInput.Disable();
            _gameStateMachine.SwitchState(GameStateType.SaveData);
            _continueGameService.DeleteContinueData();
            _uiStateMachine.SwitchStateWithoutExitFromPrevious(UIStateType.GameOver).Forget();
        }

        public void Exit()
        {
        }
    }
}