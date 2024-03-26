using _Assets.Scripts.Services.Configs;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class UIThemeChangedColor : MonoBehaviour
    {
        [SerializeField] private Selectable selectable;
        [Inject] private UIThemeService _uiThemeService;

        private void Start()
        {
            ThemeChanged(_uiThemeService.CurrentTheme);
            _uiThemeService.OnThemeChanged += ThemeChanged;
        }

        private void ThemeChanged(UITheme theme)
        {
            var selectableColors = selectable.colors;
            selectableColors.selectedColor = theme.Selected;
            selectableColors.normalColor = theme.Unselected;
            selectable.colors = selectableColors;
        }

        private void OnDestroy() => _uiThemeService.OnThemeChanged -= ThemeChanged;
    }
}