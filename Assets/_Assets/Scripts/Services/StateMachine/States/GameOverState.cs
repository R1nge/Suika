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
        private readonly YandexService _yandexService;
        private readonly LeaderBoardService _leaderBoardService;

        public GameOverState(GameStateMachine gameStateMachine, UIStateMachine uiStateMachine, PlayerInput playerInput, YandexService yandexService, LeaderBoardService leaderBoardService)
        {
            _gameStateMachine = gameStateMachine;
            _uiStateMachine = uiStateMachine;
            _playerInput = playerInput;
            _yandexService = yandexService;
            _leaderBoardService = leaderBoardService;
        }

        public async UniTask Enter()
        {
            _playerInput.Disable();
            _leaderBoardService.SetScore();
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