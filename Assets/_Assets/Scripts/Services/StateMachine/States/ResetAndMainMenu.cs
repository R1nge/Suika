using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Yandex;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class ResetAndMainMenu : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ResetService _resetService;
        private readonly UIStateMachine _uiStateMachine;
     
        private readonly ContinueGameService _continueGameService;
        private readonly TimeRushTimer _timeRushTimer;
        private readonly YandexService _yandexService;

        public ResetAndMainMenu(GameStateMachine gameStateMachine, ResetService resetService, UIStateMachine uiStateMachine, ContinueGameService continueGameService, TimeRushTimer timeRushTimer, YandexService yandexService)
        {
            _gameStateMachine = gameStateMachine;
            _resetService = resetService;
            _uiStateMachine = uiStateMachine;
            _continueGameService = continueGameService;
            _timeRushTimer = timeRushTimer;
            _yandexService = yandexService;
        }
        
        public async UniTask Enter()
        {
            //await _continueGameService.Save();
            _timeRushTimer.Reset();
            _resetService.Reset();
            await _uiStateMachine.SwitchStateAndExitFromAllPrevious(UIStateType.MainMenu);
             _yandexService.ShowVideoAd();
        }

        public void Exit()
        {
        }
    }
}