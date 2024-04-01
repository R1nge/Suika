using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GamePauseState : IGameState
    {
        private readonly PlayerInput _playerInput;
        private readonly UIStateMachine _uiStateMachine;
        private readonly TimeRushTimer _timeRushTimer;

        public GamePauseState(PlayerInput playerInput, UIStateMachine uiStateMachine, TimeRushTimer timeRushTimer)
        {
            _playerInput = playerInput;
            _uiStateMachine = uiStateMachine;
            _timeRushTimer = timeRushTimer;
        }

        public async UniTask Enter()
        {
            _playerInput.Disable();
            _timeRushTimer.Pause();
            await _uiStateMachine.SwitchStateWithoutExitFromPrevious(UIStateType.Pause);
        }

        public void Exit()
        {
        }
    }
}