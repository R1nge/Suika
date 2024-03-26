using System;
using _Assets.Scripts.Services.Configs;

namespace _Assets.Scripts.Services.UIs
{
    public class UIThemeService
    {
        private readonly ConfigProvider _configProvider;
        private Theme _theme = Theme.Dark;
        public UITheme CurrentTheme => _theme == Theme.Dark ? _configProvider.DarkTheme : _configProvider.LightTheme;
        public Theme CurrentThemeEnum => _theme;
        public event Action<UITheme> OnThemeChanged;

        private UIThemeService(ConfigProvider configProvider) => _configProvider = configProvider;

        public void SwitchTheme(Theme theme)
        {
            _theme = theme;
            OnThemeChanged?.Invoke(CurrentTheme);
        }

        public enum Theme
        {
            Light,
            Dark
        }
    }
}