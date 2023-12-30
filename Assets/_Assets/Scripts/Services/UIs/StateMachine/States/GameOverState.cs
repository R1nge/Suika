using System;
using _Assets.Scripts.Services.Configs;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class GameOverState : IUIState
    {
        private readonly ConfigProvider _configProvider;

        public GameOverState(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }
        
        public void Enter()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}