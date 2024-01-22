using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.Datas.Mods;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class InitState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AudioService _audioService;
        private readonly IAudioSettingsLoader _audioSettingsLoader;
        private readonly IConfigLoader _configLoader;
        private readonly IModDataLoader _modDataLoader;
        private readonly UIStateMachine _uiStateMachine;
        private readonly VibrationService _vibrationService;
        
        public InitState(GameStateMachine stateMachine, AudioService audioService, IAudioSettingsLoader audioSettingsLoader, IConfigLoader configLoader, IModDataLoader modDataLoader, UIStateMachine uiStateMachine, VibrationService vibrationService)
        {
            _stateMachine = stateMachine;
            _audioService = audioService;
            _audioSettingsLoader = audioSettingsLoader;
            _configLoader = configLoader;
            _modDataLoader = modDataLoader;
            _uiStateMachine = uiStateMachine;
            _vibrationService = vibrationService;
        }

        public async void Enter()
        {
            _audioService.ChangeSoundVolume(_audioSettingsLoader.AudioData.VFXVolume);
            _audioService.ChangeMusicVolume(_audioSettingsLoader.AudioData.MusicVolume);
            _configLoader.SetCurrentConfig(_modDataLoader.ModData.SelectedModName);
            _vibrationService.Init();
            await _uiStateMachine.SwitchState(UIStateType.MainMenu, 1000);
        }

        public void Exit()
        {
        }
    }
}