using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.Providers;
using _Assets.Scripts.Services.UIs.StateMachine.States;
using VContainer;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStatesFactory
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly LoadingCurtainIconProvider _loadingCurtainIconProvider;
        private readonly MainMenuProvider _mainMenuProvider;

        private UIStatesFactory(ConfigProvider configProvider, IObjectResolver objectResolver, LoadingCurtainIconProvider loadingCurtainIconProvider, MainMenuProvider mainMenuProvider)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
            _loadingCurtainIconProvider = loadingCurtainIconProvider;
            _mainMenuProvider = mainMenuProvider;
        }

        public IUIState CreateLoadingState(UIStateMachine stateMachine)
        {
            return new LoadingState(_configProvider, _objectResolver, _loadingCurtainIconProvider);
        }

        public IUIState CreateMainMenuState(UIStateMachine stateMachine)
        {
            return new MainMenuState(_configProvider, _objectResolver, _mainMenuProvider);
        }

        public IUIState CreateGameState(UIStateMachine stateMachine)
        {
            return new GameState(_configProvider, _objectResolver);
        }

        public IUIState CreateGameOverState(UIStateMachine stateMachine)
        {
            return new GameOverState(_configProvider, _objectResolver);
        }

        public IUIState CreateModsState(UIStateMachine stateMachine)
        {
            return new ModsState(_configProvider, _objectResolver);
        }

        public IUIState CreateSettingsState(UIStateMachine stateMachine)
        {
            return new SettingsState(_configProvider, _objectResolver);
        }

        public IUIState CreateGamePauseState(UIStateMachine stateMachine)
        {
            return new PauseState(_configProvider, _objectResolver);
        }
    }
}