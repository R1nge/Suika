using _Assets.Scripts.Services.GameModes;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class ResetAndRetry : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ResetService _resetService;
        private readonly TimeRushTimer _timeRushTimer;
        private readonly GameModeService _gameModeService;

        public ResetAndRetry(GameStateMachine gameStateMachine, ResetService resetService, TimeRushTimer timeRushTimer, GameModeService gameModeService)
        {
            _gameStateMachine = gameStateMachine;
            _resetService = resetService;
            _timeRushTimer = timeRushTimer;
            _gameModeService = gameModeService;
        }

        public async UniTask Enter()
        {
            _resetService.Reset();

            if (_gameModeService.SelectedGameMode == GameModeService.GameMode.TimeRush)
            {
                _timeRushTimer.Reset();
                await _gameStateMachine.SwitchState(GameStateType.TimeRush);
                return;
            }
            
            await _gameStateMachine.SwitchState(GameStateType.Classic);
        }

        public void Exit()
        {
        }
    }
}