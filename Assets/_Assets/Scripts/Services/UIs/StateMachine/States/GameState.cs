using _Assets.Scripts.Services.Configs;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class GameState : IUIState
    {
        private readonly ConfigProvider _configProvider;

        public GameState(ConfigProvider configProvider)
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