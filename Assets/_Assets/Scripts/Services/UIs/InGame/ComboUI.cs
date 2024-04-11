using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class ComboUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI comboText;
        [SerializeField] private LocalizedString comboString;
        [Inject] private ComboService _comboService;

        private void Start()
        {
            _comboService.OnComboChanged += UpdateUI;
            UpdateUI(0);
        }

        private async void UpdateUI(int combo)
        {
            var comboString = this.comboString.GetLocalizedStringAsync();
            var localizedString = await comboString.Task;
            comboText.text = $"{localizedString}: {combo}";
        }

        private void OnDestroy() => _comboService.OnComboChanged -= UpdateUI;
    }
}