using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [Inject] private ScoreService _scoreService;

        public void Init() => _scoreService.OnScoreChanged += ScoreChanged;
        private void ScoreChanged(int score) => scoreText.text = score.ToString();

        private void OnDestroy() => _scoreService.OnScoreChanged -= ScoreChanged;
    }
    
}