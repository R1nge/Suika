using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

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

        public async UniTask Enter()
        {
            _playerInput.Disable();
            await _uiStateMachine.SwitchStateWithoutExitFromPrevious(UIStateType.Pause);
        }

        public void Exit()
        {
        }
    }
}