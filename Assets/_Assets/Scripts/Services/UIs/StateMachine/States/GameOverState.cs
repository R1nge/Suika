using _Assets.Scripts.Services.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class GameOverState : IUIState
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private GameObject _ui;

        public GameOverState(ConfigProvider configProvider, IObjectResolver objectResolver)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
        }

        public void Enter() => _ui = _objectResolver.Instantiate(_configProvider.UIConfig.GameOverMenu);

        public void Exit() => Object.Destroy(_ui);
    }
}