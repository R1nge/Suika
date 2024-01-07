using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.Providers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class LoadingState : IUIState
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly LoadingCurtainIconProvider _loadingCurtainIconProvider;
        private GameObject _ui;
        
        public LoadingState(ConfigProvider configProvider, IObjectResolver objectResolver, LoadingCurtainIconProvider loadingCurtainIconProvider)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
            _loadingCurtainIconProvider = loadingCurtainIconProvider;
        }

        public async void Enter()
        {
            await _loadingCurtainIconProvider.Load();
            _ui = _objectResolver.Instantiate(_configProvider.UIConfig.LoadingMenu);
            _ui.GetComponent<LoadingCurtain>().Init(_loadingCurtainIconProvider.BackgroundSprite, _loadingCurtainIconProvider.IconSprite);
            _ui.GetComponent<LoadingCurtain>().Show();
        }

        public async void Exit(int millisecondsDelay)
        {
            await UniTask.Delay(millisecondsDelay);
            _ui.GetComponent<LoadingCurtain>().Hide();
        }
    }
}