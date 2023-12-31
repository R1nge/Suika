using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class InGameTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [Inject] private GameOverTimer _gameOverTimer;

        private void Start()
        {
            _gameOverTimer.OnTimerStarted += TimerStarted;
            _gameOverTimer.OnTimeChanged += TimeChanged;
            _gameOverTimer.OnTimerStopped += TimerStopped;
        }
        
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