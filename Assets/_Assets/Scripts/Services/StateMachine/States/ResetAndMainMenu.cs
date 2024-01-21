using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class ResetAndMainMenu : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ResetService _resetService;
        private readonly UIStateMachine _uiStateMachine;
        private readonly AudioService _audioService;

        public ResetAndMainMenu(GameStateMachine gameStateMachine, ResetService resetService, UIStateMachine uiStateMachine, AudioService audioService)
        {
            _gameStateMachine = gameStateMachine;
            _resetService = resetService;
            _uiStateMachine = uiStateMachine;
            _audioService = audioService;
        }
        
        public void Enter()
        {
            _resetService.Reset();
            _uiStateMachine.SwitchStateAndExitFromAllPrevious(UIStateType.MainMenu).Forget();
            _audioService.StopMusic();
            _audioService.ResetIndex();
        }

        public void Exit()
        {
        }
    }
}