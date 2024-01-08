using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameOverState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly PlayerInput _playerInput;

        public GameOverState(GameStateMachine gameStateMachine, UIStateMachine uiStateMachine, PlayerInput playerInput)
        {
            _gameStateMachine = gameStateMachine;
            _uiStateMachine = uiStateMachine;
            _playerInput = playerInput;
        }

        public void Enter()
        {
            _playerInput.Disable();
            _uiStateMachine.SwitchState(UIStateType.GameOver).Forget();
            _gameStateMachine.SwitchState(GameStateType.SaveData);
        }

        public void Exit()
        {
        }
    }
}