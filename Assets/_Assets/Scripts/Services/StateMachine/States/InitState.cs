using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.Datas.Mods;
using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Vibrations;
using _Assets.Scripts.Services.Yandex;
using Cysharp.Threading.Tasks;
using YG;

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
        private readonly PlayerInput _playerInput;
        private readonly YandexService _yandexService;
        private readonly LocalizationService _localizationService;

        public InitState(GameStateMachine stateMachine, AudioService audioService,
            IAudioSettingsLoader audioSettingsLoader, IConfigLoader configLoader, IModDataLoader modDataLoader,
            UIStateMachine uiStateMachine, VibrationService vibrationService, PlayerInput playerInput,
            YandexService yandexService, LocalizationService localizationService)
        {
            _stateMachine = stateMachine;
            _audioService = audioService;
            _audioSettingsLoader = audioSettingsLoader;
            _configLoader = configLoader;
            _modDataLoader = modDataLoader;
            _uiStateMachine = uiStateMachine;
            _vibrationService = vibrationService;
            _playerInput = playerInput;
            _yandexService = yandexService;
            _localizationService = localizationService;
        }

        public async UniTask Enter()
        {
            //_audioService.ChangeSoundVolume(_audioSettingsLoader.AudioData.VFXVolume);
            //_audioService.ChangeMusicVolume(_audioSettingsLoader.AudioData.MusicVolume);
            _configLoader.SetCurrentConfig(_modDataLoader.ModData.SelectedModName);
            _vibrationService.Init();
            _playerInput.Init();
            _audioService.Init();
            await _localizationService.InitYandex(YandexGame.lang);
            await _uiStateMachine.SwitchState(UIStateType.MainMenu, 1000);
            await _yandexService.Init();
        }

        public void Exit()
        {
        }
    }
}