using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class ResetAndMainMenu : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ResetService _resetService;
        private readonly UIStateMachine _uiStateMachine;

        public ResetAndMainMenu(GameStateMachine gameStateMachine, ResetService resetService, UIStateMachine uiStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _resetService = resetService;
            _uiStateMachine = uiStateMachine;
        }
        
        public void Enter()
        {
            _resetService.Reset();
            _uiStateMachine.SwitchState(UIStateType.MainMenu).Forget();
        }

        public void Exit()
        {
        }
    }
}