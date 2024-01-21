using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.Providers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class SettingsState : IUIState
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly MainMenuProvider _mainMenuProvider;
        private GameObject _ui;

        public SettingsState(ConfigProvider configProvider, IObjectResolver objectResolver, MainMenuProvider mainMenuProvider)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
            _mainMenuProvider = mainMenuProvider;
        }

        public UniTask Enter()
        {
            _ui = _objectResolver.Instantiate(_configProvider.UIConfig.SettingMenu);
            _ui.GetComponent<SettingsMenu>().Init(_mainMenuProvider.BackgroundSprite);
            return UniTask.CompletedTask;
        }

        public void Exit() => Object.Destroy(_ui);
    }
}