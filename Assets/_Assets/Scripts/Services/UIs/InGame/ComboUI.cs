using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class ComboUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI comboText;
        [Inject] private ComboService _comboService;

        private void Start()
        {
            _comboService.OnComboChanged += UpdateUI;
            UpdateUI(0);
        }

        private void UpdateUI(int combo) => comboText.text = $"Combo: {combo}";

        private void OnDestroy() => _comboService.OnComboChanged -= UpdateUI;
    }
}