using _Assets.Scripts.Services.Yandex;
using YG.Utils.LB;

namespace _Assets.Scripts.Services
{
    public class LeaderBoardService
    {
        private readonly YandexService _yandexService;
        private readonly ScoreService _scoreService;
        private LBData _lbData;
        public LBData LbData => _lbData;

        private LeaderBoardService(YandexService yandexService, ScoreService scoreService)
        {
            _yandexService = yandexService;
            _scoreService = scoreService;
        }

        public void Init()
        {
            _yandexService.OnGetLeaderboard += GetLB;
            _yandexService.GetLeaderBoard();
        }

        public void GetLb()
        {
            _yandexService.GetLeaderBoard();
        }

        private void GetLB(LBData data)
        {
            _lbData = data;
            SetScore();
        }

        public void SetScore()
        {
            if (_lbData.thisPlayer.score < _scoreService.Score)
            {
                _lbData.thisPlayer.score = _scoreService.Score;
                _yandexService.UpdateLeaderBoardScore(_scoreService.Score);
            }
        }

        public void Destroy()
        {
            _yandexService.OnGetLeaderboard -= GetLB;
        }
    }
}