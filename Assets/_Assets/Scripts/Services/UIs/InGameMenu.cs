using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Image nextSuikaImage;

        [Inject] private GameOverTimer _gameOverTimer;
        [Inject] private SuikaDataProvider _suikaDataProvider;

        [Inject] private RandomNumberGenerator _randomNumberGenerator;
        //TODO: show next suika
        //TODO: show score
        //TODO: create a service for that 

        private void Start()
        {
            _gameOverTimer.OnTimerStarted += TimerStarted;
            _gameOverTimer.OnTimeChanged += TimeChanged;
            _gameOverTimer.OnTimerStopped += TimerStopped;

            _randomNumberGenerator.OnSuikaPicked += NextSuikaPicked;
        }

        private void NextSuikaPicked(int previous, int current, int next) => nextSuikaImage.sprite = _suikaDataProvider.GetNextSuika();

        private void TimerStarted(float startTime, float currentTime) => timerText.text = currentTime.ToString("0.0");

        private void TimeChanged(float currentTime) => timerText.text = currentTime.ToString("0.0");

        private void TimerStopped(float startTime) => timerText.text = string.Empty;

        private void OnDestroy()
        {
            _gameOverTimer.OnTimerStarted -= TimerStarted;
            _gameOverTimer.OnTimeChanged -= TimeChanged;
            _gameOverTimer.OnTimerStopped -= TimerStopped;
        }
    }
}