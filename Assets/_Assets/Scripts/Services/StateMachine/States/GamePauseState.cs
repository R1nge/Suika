using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GamePauseState : IGameState
    {
        private readonly PlayerInput _playerInput;
        private readonly UIStateMachine _uiStateMachine;

        public GamePauseState(PlayerInput playerInput, UIStateMachine uiStateMachine)
        {
            _playerInput = playerInput;
            _uiStateMachine = uiStateMachine;
        }

        public void Enter()
        {
            _uiStateMachine.SwitchStateWithoutExitFromPrevious(UIStateType.Pause).Forget();
            _playerInput.Disable();
        }

        public void Exit()
        {
        }
    }
}