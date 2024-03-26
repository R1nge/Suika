using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class ChangeThemeButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [Inject] private UIThemeService _uiThemeService;

        private void Start() => button.onClick.AddListener(ChangeTheme);

        private void ChangeTheme()
        {
            var newTheme = _uiThemeService.CurrentThemeEnum == UIThemeService.Theme.Dark
                ? UIThemeService.Theme.Light
                : UIThemeService.Theme.Dark;
            _uiThemeService.SwitchTheme(newTheme);
        }

        private void OnDestroy() => button.onClick.RemoveAllListeners();
    }
}