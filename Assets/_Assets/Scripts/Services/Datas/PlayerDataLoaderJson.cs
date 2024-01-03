using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace _Assets.Scripts.Services.Datas
{
    public class PlayerDataLoaderJson : IPlayerDataLoader
    {
        private readonly ScoreService _scoreService;
        private List<GameData> _gameDatas = new(5);
        public List<GameData> GameDatas => _gameDatas;
        private const string DataFileName = "playerData.json";
        private readonly string _dataPath = Path.Combine(Application.persistentDataPath, "Data");

        public PlayerDataLoaderJson(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

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
            
            var json = JsonConvert.SerializeObject(dataToSave);

            var path = Path.Combine(_dataPath, DataFileName);
            
            if(!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
            }
            
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(json);
            }
        }

        public void LoadData()
        {
            var path = Path.Combine(_dataPath, DataFileName);
            
            if (!File.Exists(path))
            {
                Debug.LogWarning("Score data not found");
                return;
            }

            using (StreamReader reader = new StreamReader(Path.Combine(_dataPath, DataFileName)))
            {
                var json = reader.ReadToEnd();
                var gameDatas = JsonConvert.DeserializeObject<List<GameData>>(json);
                _gameDatas = gameDatas;
                _gameDatas.Capacity = 5;
            }
        }
    }
}