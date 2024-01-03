using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _Assets.Scripts.Services.Datas.GameConfigs
{
    public class GameConfigLoaderJson : IConfigLoader
    {
        private readonly GameConfigValidator _gameConfigValidator;
        private GameConfig _currentConfig;
        private readonly List<GameConfig> _allConfigs = new();
        private readonly string _modsPath = Path.Combine(Application.persistentDataPath, "Mods");
        private readonly string _streamingAssetsPath = Application.streamingAssetsPath;

        public List<GameConfig> AllConfigs => _allConfigs;
        public GameConfig CurrentConfig => _currentConfig;
        public bool IsDefault => _currentConfig.Equals(_allConfigs[0]);

        public GameConfigLoaderJson(GameConfigValidator gameConfigValidator)
        {
            _gameConfigValidator = gameConfigValidator;
        }


        public async UniTask LoadDefaultConfig()
        {
            var targetFile = Path.Combine(_streamingAssetsPath, "config.json");

            UnityWebRequest www = UnityWebRequest.Get(targetFile);
            await www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var json = www.downloadHandler.text;
                var config = JsonConvert.DeserializeObject<GameConfig>(json);
                
                var modIconRelativePath = Path.Combine(_streamingAssetsPath, config.ModIconPath);
                config.ModIconPath = modIconRelativePath;

                var containerImageRelativePath = Path.Combine(_streamingAssetsPath, config.ContainerImagePath);
                config.ContainerImagePath = containerImageRelativePath;

                for (int i = 0; i < config.SuikaSkinsImagesPaths.Length; i++)
                {
                    var skinImageRelativePath = Path.Combine(_streamingAssetsPath, config.SuikaSkinsImagesPaths[i]);
                    config.SuikaSkinsImagesPaths[i] = skinImageRelativePath;
                }

                for (int i = 0; i < config.SuikaIconsPaths.Length; i++)
                {
                    var suikaIconsRelativePath = Path.Combine(_streamingAssetsPath, config.SuikaIconsPaths[i]);
                    config.SuikaIconsPaths[i] = suikaIconsRelativePath;
                }

                for (int i = 0; i < config.SuikaAudioPaths.Length; i++)
                {
                    var suikaAudioRelativePath = Path.Combine(_streamingAssetsPath, config.SuikaAudioPaths[i]);
                    config.SuikaAudioPaths[i] = suikaAudioRelativePath;
                }
                
                if (config.TimeBeforeTimerTrigger < 0)
                {
                    Debug.LogError($"TimeBeforeTimerTrigger is less than 0. Setting from default config. Value: {_allConfigs[0].TimeBeforeTimerTrigger}");
                }
                
                if (config.TimerStartTime < 0)
                {
                    Debug.LogError($"TimerStartTime is less than 0. Setting from default config. Value: {_allConfigs[0].TimerStartTime}");
                }
                
                var inGameBackground = Path.Combine(_streamingAssetsPath, config.InGameBackgroundPath);
                config.InGameBackgroundPath = inGameBackground;

                _allConfigs.Add(config);

                await UniTask.Delay(100);
            }
        }

        public void LoadAllConfigs()
        {
            var modsFoldersDirectoryInfo = new DirectoryInfo(_modsPath);

            if (!modsFoldersDirectoryInfo.Exists)
            {
                modsFoldersDirectoryInfo.Create();
                return;
            }

            foreach (var directoryInfo in modsFoldersDirectoryInfo.GetDirectories())
            {
                foreach (var fileInfo in directoryInfo.GetFiles("*.json"))
                {
                    StreamReader reader = new StreamReader(fileInfo.FullName);
                    var json = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                    var config = JsonConvert.DeserializeObject<GameConfig>(json);

                    var defaultConfig = _allConfigs[0];
                    _gameConfigValidator.Validate(ref defaultConfig, ref config);

                    _allConfigs.Add(config);
                }
            }

            _currentConfig = _allConfigs[0];
        }

       

        public void SetCurrentConfig(int index) => _currentConfig = _allConfigs[index];
    }
}