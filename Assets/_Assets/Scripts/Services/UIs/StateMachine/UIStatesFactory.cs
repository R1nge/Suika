using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.UIs.StateMachine.States;
using VContainer;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStatesFactory
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;

        private UIStatesFactory(ConfigProvider configProvider, IObjectResolver objectResolver)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
        }

        public IUIState CreateLoadingState(UIStateMachine stateMachine)
        {
            return new LoadingState(_configProvider, _objectResolver);
        }

        public IUIState CreateMainMenuState(UIStateMachine stateMachine)
        {
            return new MainMenuState(_configProvider, _objectResolver);
        }

        public IUIState CreateGameState(UIStateMachine stateMachine)
        {
            return new GameState(_configProvider, _objectResolver);
        }

        public IUIState CreateGameOverState(UIStateMachine stateMachine)
        {
            return new GameOverState(_configProvider, _objectResolver);
        }
    }
}