using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Datas.Mods;
using _Assets.Scripts.Services.UIs.StateMachine.States;
using _Assets.Scripts.Services.Vibrations;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStatesFactory
    {
        private readonly UIFactory _uiFactory;
        private readonly IModDataLoader _modDataLoader;
        private readonly ContinueGameService _continueGameService;
        private readonly IAudioSettingsLoader _audioSettingsLoader;
        private readonly IVibrationSettingLoader _vibrationSettingLoader;

        private UIStatesFactory(UIFactory uiFactory, IModDataLoader modDataLoader, ContinueGameService continueGameService, IAudioSettingsLoader audioSettingsLoader, IVibrationSettingLoader vibrationSettingLoader)
        {
            _uiFactory = uiFactory;
            _modDataLoader = modDataLoader;
            _continueGameService = continueGameService;
            _audioSettingsLoader = audioSettingsLoader;
            _vibrationSettingLoader = vibrationSettingLoader;
        }

        public IUIState CreateLoadingState(UIStateMachine stateMachine) => new LoadingState(_uiFactory);

        public IUIState CreateMainMenuState(UIStateMachine stateMachine) => new MainMenuState(_uiFactory, _continueGameService, _modDataLoader);

        public IUIState CreateGameState(UIStateMachine stateMachine) => new GameState(_uiFactory);

        public IUIState CreateGameOverState(UIStateMachine stateMachine) => new GameOverState(_uiFactory);

        public IUIState CreateModsState(UIStateMachine stateMachine) => new ModsState(_uiFactory);

        public IUIState CreateSettingsState(UIStateMachine stateMachine) => new SettingsState(_uiFactory, _audioSettingsLoader, _vibrationSettingLoader);

        public IUIState CreateGamePauseState(UIStateMachine stateMachine) => new PauseState(_uiFactory);
    }
}