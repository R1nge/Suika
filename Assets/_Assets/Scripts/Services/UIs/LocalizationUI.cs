using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class LocalizationUI : MonoBehaviour
    {
        [SerializeField] private Button en, ru, tr;
        [Inject] private LocalizationService _localizationService;

        private void Start()
        {
            en.onClick.AddListener(English);
            ru.onClick.AddListener(Russian);
            tr.onClick.AddListener(Turkish);
        }

        private async void English()
        {
            await _localizationService.SetLanguage(LocalizationService.Language.English);
        }

        private async void Russian()
        {
            await _localizationService.SetLanguage(LocalizationService.Language.Russian);
        }

        private async void Turkish()
        {
            await _localizationService.SetLanguage(LocalizationService.Language.Turkish);
        }

        private void OnDestroy()
        {
            en.onClick.RemoveListener(English);
            ru.onClick.RemoveListener(Russian);
            tr.onClick.RemoveListener(Turkish);
        }
    }
}