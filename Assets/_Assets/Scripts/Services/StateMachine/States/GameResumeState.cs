using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.GameModes;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameResumeState : IGameState
    {
        private readonly PlayerInput _playerInput;
        private readonly UIStateMachine _uiStateMachine;
        private readonly IAudioSettingsLoader _audioSettingsLoader;
        private readonly TimeRushTimer _timeRushTimer;
        private readonly GameModeService _gameModeService;

        public GameResumeState(PlayerInput playerInput, UIStateMachine uiStateMachine,
            IAudioSettingsLoader audioSettingsLoader, TimeRushTimer timeRushTimer, GameModeService gameModeService)
        {
            _playerInput = playerInput;
            _uiStateMachine = uiStateMachine;
            _audioSettingsLoader = audioSettingsLoader;
            _timeRushTimer = timeRushTimer;
            _gameModeService = gameModeService;
        }

        public async UniTask Enter()
        {
            await _uiStateMachine.SwitchState(UIStateType.Game);
            await UniTask.DelayFrame(1);
            
            if (_gameModeService.SelectedGameMode == GameModeService.GameMode.TimeRush)
            {
                _timeRushTimer.Resume();
            }

            _playerInput.Enable();
        }

        public void Exit() => _audioSettingsLoader.Save();
    }
}