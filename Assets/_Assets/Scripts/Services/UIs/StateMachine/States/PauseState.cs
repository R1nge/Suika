using _Assets.Scripts.Services.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class PauseState : IUIState
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private GameObject _ui;
        
        public PauseState(ConfigProvider configProvider, IObjectResolver objectResolver)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
        }
        
        public async UniTask Enter() => _ui = _objectResolver.Instantiate(_configProvider.UIConfig.PauseMenu);

        public void Exit() => Object.Destroy(_ui);
    }
}