using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.Providers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class MainMenuState : IUIState
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly MainMenuProvider _mainMenuProvider;
        private GameObject _ui;
        
        public MainMenuState(ConfigProvider configProvider, IObjectResolver objectResolver, MainMenuProvider mainMenuProvider)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
            _mainMenuProvider = mainMenuProvider;
        }

        public async void Enter()
        {
            await _mainMenuProvider.Load();
            _ui = _objectResolver.Instantiate(_configProvider.UIConfig.MainMenu);
            _ui.GetComponent<MainMenu>().Init(_mainMenuProvider.BackgroundSprite);
        }

        public void Exit() => Object.Destroy(_ui);
    }
}