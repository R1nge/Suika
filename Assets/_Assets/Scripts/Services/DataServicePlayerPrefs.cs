using UnityEngine;

namespace _Assets.Scripts.Services
{
    public class DataServicePlayerPrefs : IDataService
    {
        private readonly ScoreService _scoreService;
        private GameData _gameData;
        private const string DataKey = "Data";

        public DataServicePlayerPrefs(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void SaveData()
        {
            _gameData = new GameData(_scoreService.Score);
            var dataJson = JsonUtility.ToJson(_gameData);
            PlayerPrefs.SetString(DataKey, dataJson);
            PlayerPrefs.Save();
        }

        public void LoadData()
        {
            var dataJson = PlayerPrefs.GetString(DataKey);

            if (string.IsNullOrEmpty(dataJson))
            {
                Debug.LogWarning("Data not found");
                return;
            }

            _gameData = JsonUtility.FromJson<GameData>(dataJson);
        }
    }

    public struct GameData
    {
        public int Score;

        public GameData(int score)
        {
            Score = score;
        }
    }
}