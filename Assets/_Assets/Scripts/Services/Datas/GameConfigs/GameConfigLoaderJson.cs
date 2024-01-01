using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace _Assets.Scripts.Services.Datas.GameConfigs
{
    public class GameConfigLoaderJson : IConfigLoader
    {
        private GameConfig _gameConfig;
        private readonly string _path = $"{Application.persistentDataPath}/config.json";

        public GameConfig GameConfig => _gameConfig;
        public GameConfig LoadConfig()
        {
            Debug.LogError($"Path: {_path}");
            using (StreamReader file = File.OpenText(_path))
            {
                GameConfig gameConfig = JsonConvert.DeserializeObject<GameConfig>(file.ReadToEnd());
                Debug.LogError($"Path: {_path} DATA: {gameConfig}");
                return _gameConfig = gameConfig;
            }
        }
    }
}