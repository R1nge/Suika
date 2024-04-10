using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Yandex;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameOverState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly PlayerInput _playerInput;
        private readonly ContinueGameService _continueGameService;
        private readonly YandexService _yandexService;

        public GameOverState(GameStateMachine gameStateMachine, UIStateMachine uiStateMachine, PlayerInput playerInput, ContinueGameService continueGameService, YandexService yandexService)
        {
            _gameStateMachine = gameStateMachine;
            _uiStateMachine = uiStateMachine;
            _playerInput = playerInput;
            _continueGameService = continueGameService;
            _yandexService = yandexService;
        }

        public async UniTask Enter()
        {
            _playerInput.Disable();
            await _gameStateMachine.SwitchState(GameStateType.SaveData);
            //_continueGameService.DeleteContinueData();
            await _uiStateMachine.SwitchStateWithoutExitFromPrevious(UIStateType.GameOver);
            _yandexService.ShowVideoAd();
        }

        public void Exit()
        {
        }
    }
}