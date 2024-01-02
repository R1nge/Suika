using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class NextSuikaUI : MonoBehaviour
    {
        [SerializeField] private Image nextSuikaImage;
        [Inject] private RandomNumberGenerator _randomNumberGenerator;
        [Inject] private SuikaUIDataProvider _suikaUIDataProvider;

        private void Start() => _randomNumberGenerator.OnSuikaPicked += NextSuikaPicked;

        private async void NextSuikaPicked(int previous, int current, int next)
        {
            nextSuikaImage.sprite = await _suikaUIDataProvider.GetNextSuika();
        }

        private void OnDestroy() => _randomNumberGenerator.OnSuikaPicked -= NextSuikaPicked;
    }
}