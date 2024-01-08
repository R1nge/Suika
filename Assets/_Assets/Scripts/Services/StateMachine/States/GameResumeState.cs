using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameResumeState : IGameState
    {
        private readonly PlayerInput _playerInput;
        private readonly UIStateMachine _uiStateMachine;

        public GameResumeState(PlayerInput playerInput, UIStateMachine uiStateMachine)
        {
            _playerInput = playerInput;
            _uiStateMachine = uiStateMachine;
        }

        public async void Enter()
        {
            _uiStateMachine.SwitchState(UIStateType.Game).Forget();
            await UniTask.DelayFrame(1);
            _playerInput.Enable();
        }

        public void Exit()
        {
        }
    }
}