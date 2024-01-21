using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.Datas.Mods;
using _Assets.Scripts.Services.Datas.Player;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class LoadSaveDataState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPlayerDataLoader _playerDataLoader;
        private readonly UIStateMachine _uiStateMachine;
        private readonly IConfigLoader _configLoader;
        private readonly IModDataLoader _modDataLoader;
        private readonly IAudioSettingsLoader _audioSettingsLoader;
        private readonly ContinueGameService _continueGameService;
        private readonly AudioService _audioService;

        public LoadSaveDataState(GameStateMachine stateMachine, IPlayerDataLoader playerDataLoader, UIStateMachine uiStateMachine, IConfigLoader configLoader, IModDataLoader modDataLoader, IAudioSettingsLoader audioSettingsLoader, ContinueGameService continueGameService, AudioService audioService)
        {
            _stateMachine = stateMachine;
            _playerDataLoader = playerDataLoader;
            _uiStateMachine = uiStateMachine;
            _configLoader = configLoader;
            _modDataLoader = modDataLoader;
            _audioSettingsLoader = audioSettingsLoader;
            _continueGameService = continueGameService;
            _audioService = audioService;
        }

        public async void Enter()
        {
            await _continueGameService.Load();
            await _audioSettingsLoader.Load();
            _audioService.ChangeSoundVolume(_audioSettingsLoader.AudioData.VFXVolume);
            _audioService.ChangeMusicVolume(_audioSettingsLoader.AudioData.MusicVolume);
            await _modDataLoader.Load();
            _playerDataLoader.LoadData();
            await _configLoader.LoadDefaultConfig();
            _configLoader.LoadAllConfigs();
            _configLoader.SetCurrentConfig(_modDataLoader.ModData.SelectedModName);
            await _uiStateMachine.SwitchState(UIStateType.Loading);
            await _uiStateMachine.SwitchState(UIStateType.MainMenu, 1000);
        }

        public void Exit()
        {
        }
    }
}