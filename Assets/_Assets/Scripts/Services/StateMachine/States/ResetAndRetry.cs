using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class ResetAndRetry : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ResetService _resetService;
        private readonly TimeRushTimer _timeRushTimer;

        public ResetAndRetry(GameStateMachine gameStateMachine, ResetService resetService, TimeRushTimer timeRushTimer)
        {
            _gameStateMachine = gameStateMachine;
            _resetService = resetService;
            _timeRushTimer = timeRushTimer;
        }

        public async UniTask Enter()
        {
            _timeRushTimer.Stop();
            _resetService.Reset();
            await _gameStateMachine.SwitchState(GameStateType.Classic);
        }

        public void Exit()
        {
        }
    }
}