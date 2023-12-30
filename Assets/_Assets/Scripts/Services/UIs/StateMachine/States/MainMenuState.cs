using _Assets.Scripts.Services.Configs;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class MainMenuState : IUIState
    {
        private readonly ConfigProvider _configProvider;

        public MainMenuState(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }
        
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}