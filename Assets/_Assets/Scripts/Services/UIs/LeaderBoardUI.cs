using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class LeaderBoardUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] scores;
        [Inject] private LeaderBoardService _leaderBoardService;

        private void Start()
        {
            for (int i = 0; i < scores.Length; i++)
            {
                if (_leaderBoardService.LbData.players[i] != null)
                {
                    scores[i].text = _leaderBoardService.LbData.players[i].score.ToString();       
                }
            }
        }
    }
}