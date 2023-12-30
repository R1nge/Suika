using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.UIs.StateMachine.States;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStatesFactory
    {
        private readonly ConfigProvider _configProvider;

        private UIStatesFactory(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }
        public IUIState CreateLoadingState(UIStateMachine stateMachine)
        {
            return new LoadingState(_configProvider);
        }
        
        public IUIState CreateMainMenuState(UIStateMachine stateMachine)
        {
            return new MainMenuState(_configProvider);
        }
        
        public IUIState CreateGameState(UIStateMachine stateMachine)
        {
            return new GameState(_configProvider);
        }
        
        public IUIState CreateGameOverState(UIStateMachine stateMachine)
        {
            return new GameOverState(_configProvider);
            
        }
    }
}