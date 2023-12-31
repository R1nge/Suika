using System.Collections.Generic;
using System.Linq;
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
            var data = new GameData(_scoreService.Score);
            _gameDatas.Add(data);

            _gameDatas = _gameDatas.OrderByDescending(gameData => gameData).ToList();

            var dataToSave = new List<GameData>(5);

            for (int i = 0; i < _gameDatas.Count; i++)
            {
                dataToSave.Add(_gameDatas[i]);
            }
            
            var dataJson = JsonConvert.SerializeObject(dataToSave);
            PlayerPrefs.SetString(DataKeyBase, dataJson);
            PlayerPrefs.Save();
            Debug.LogError("Saved data");
        }

        public void LoadData()
        {
            var dataJson = PlayerPrefs.GetString(DataKeyBase);

            if (string.IsNullOrEmpty(dataJson) || dataJson == "{}")
            {
                Debug.LogWarning("Data not found");
                return;
            }

            var data = JsonConvert.DeserializeObject<List<GameData>>(dataJson);

            var dataSorted = data.OrderByDescending(gameData => gameData).ToArray();

            var length = _gameDatas.Capacity > dataSorted.Length ?  dataSorted.Length : _gameDatas.Capacity;

            for (int i = 0; i < length; i++)
            {
                _gameDatas.Add(dataSorted[i]);
            }

            Debug.LogWarning("Loaded data");
        }
    }
}