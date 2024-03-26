using _Assets.Scripts.Services.Configs;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class UIThemeChangedIcon : MonoBehaviour
    {
        [SerializeField] private UITheme.UIElementType type;
        [SerializeField] private Image image;
        [Inject] private UIThemeService _uiThemeService;

        private void Start() => _uiThemeService.OnThemeChanged += OnThemeChanged;

        private void OnThemeChanged(UITheme theme)
        {
            image.sprite = theme.GetSprite(type);
        }

        private void OnDestroy() => _uiThemeService.OnThemeChanged -= OnThemeChanged;
    }
}