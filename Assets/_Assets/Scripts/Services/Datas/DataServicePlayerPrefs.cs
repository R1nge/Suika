using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace _Assets.Scripts.Services.Datas
{
    public class DataServicePlayerPrefs : IDataService
    {
        private readonly ScoreService _scoreService;
        private List<GameData> _gameDatas = new(5);
        private const string DataKeyBase = "Data";

        public List<GameData> GameDatas => _gameDatas;

        public DataServicePlayerPrefs(ScoreService scoreService) => _scoreService = scoreService;

        public void SaveData()
        {
            if (_gameDatas.Count < _gameDatas.Capacity)
            {
                _gameDatas.Add(new GameData(_scoreService.Score));
                var dataJson = JsonConvert.SerializeObject(_gameDatas[^1]);
                PlayerPrefs.SetString(DataKeyBase + (_gameDatas.Count - 1), dataJson);
            }
            else
            {
                for (int i = 0; i < _gameDatas.Count; i++)
                {
                    if (_gameDatas[i].Score < _scoreService.Score)
                    {
                        _gameDatas.Insert(i, new GameData(_scoreService.Score));
                        var dataJson = JsonUtility.ToJson(_gameDatas[i]);
                        PlayerPrefs.SetString(DataKeyBase + i, dataJson);
                        break;
                    }
                }
            }

            PlayerPrefs.Save();
            Debug.LogWarning("Saved data");
        }

        public void LoadData()
        {
           
            for (int i = 0; i < _gameDatas.Capacity; i++)
            {
                var dataJson = PlayerPrefs.GetString(DataKeyBase + i);

                if (string.IsNullOrEmpty(dataJson) || dataJson == "{}")
                {
                    Debug.LogWarning($"Data{i} not found");

                    for (int j = 0; j < _gameDatas.Count; j++)
                    {
                        Debug.LogError($"Game data{j} Score: {_gameDatas[j].Score}");
                    }
                    
                    return;
                }

                var data = JsonConvert.DeserializeObject<GameData>(dataJson);
                _gameDatas.Add(data);
            }

            Debug.LogWarning("Loaded data");

            for (int i = 0; i < _gameDatas.Count; i++)
            {
                Debug.LogError($"Game data{i} Score: {_gameDatas[i].Score}");
            }
        }
    }
}

[Serializable]
public struct GameData : IComparable<GameData>
{
    public int Score;

    public GameData(int score)
    {
        Score = score;
    }

    public int CompareTo(GameData other)
    {
        return Score.CompareTo(other.Score);
    }
}