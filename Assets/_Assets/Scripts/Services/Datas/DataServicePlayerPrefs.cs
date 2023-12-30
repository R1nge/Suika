using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Services.Datas
{
    public class DataServicePlayerPrefs : IDataService
    {
        private readonly ScoreService _scoreService;
        private List<GameData> _gameDatas = new(5);
        private const string DataKeyBase = "Data";

        public DataServicePlayerPrefs(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void SaveData()
        {
            for (int i = 0; i < _gameDatas.Count; i++)
            {
                _gameDatas[i] = new GameData(_scoreService.Score);
                var dataJson = JsonUtility.ToJson(_gameDatas);
                PlayerPrefs.SetString(DataKeyBase + i, dataJson);
            }
            
            PlayerPrefs.Save();
            Debug.LogWarning("Saved data");
        }

        public void LoadData()
        {
            Debug.LogWarning("Loading data");
            for (int i = 0; i < _gameDatas.Capacity; i++)
            {
                var dataJson = PlayerPrefs.GetString(DataKeyBase + i);

                if (string.IsNullOrEmpty(dataJson))
                {
                    Debug.LogWarning($"Data{i} not found");
                    return;
                }

                _gameDatas[i] = JsonUtility.FromJson<GameData>(dataJson);    
            }
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