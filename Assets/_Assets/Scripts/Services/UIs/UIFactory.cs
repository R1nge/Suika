using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.Providers;
using _Assets.Scripts.Services.UIs.Mods;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs
{
    public class UIFactory
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly MainMenuProvider _mainMenuProvider;

        private UIFactory(ConfigProvider configProvider, IObjectResolver objectResolver, MainMenuProvider mainMenuProvider)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
            _mainMenuProvider = mainMenuProvider;
        }

        public ModsMenu CreateModUI()
        {
            var modUI = _objectResolver.Instantiate(_configProvider.UIConfig.ModsMenu).GetComponent<ModsMenu>();
            modUI.Init(_mainMenuProvider.BackgroundSprite);
            return modUI;
        }
    }
}