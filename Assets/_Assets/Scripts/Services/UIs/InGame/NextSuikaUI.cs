using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class NextSuikaUI : MonoBehaviour
    {
        [SerializeField] private Image nextSuikaImage;
        [Inject] private RandomNumberGenerator _randomNumberGenerator;
        [Inject] private SuikaDataProvider _suikaDataProvider;

        private void Start() => _randomNumberGenerator.OnSuikaPicked += NextSuikaPicked;

        private void NextSuikaPicked(int previous, int current, int next)
        {
            nextSuikaImage.sprite = _suikaDataProvider.GetNextSuika();
        }

        private void OnDestroy() => _randomNumberGenerator.OnSuikaPicked -= NextSuikaPicked;
    }
}