using _Assets.Scripts.Services.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class LoadingState : IUIState
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private GameObject _ui;
        
        public LoadingState(ConfigProvider configProvider, IObjectResolver objectResolver)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
        }

        public void Enter()
        {
            //TODO: loading screen with a spinning suika (circle), probably with an anime image
            _ui = _objectResolver.Instantiate(_configProvider.UIConfig.LoadingMenu);
        }

        public void Exit() => _ui.GetComponent<LoadingCurtain>().Hide();
    }
}