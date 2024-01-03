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
        private GameConfig _currentConfig;
        private readonly List<GameConfig> _allConfigs = new();
        private readonly string _modsPath = Path.Combine(Application.persistentDataPath, "Mods");
        private readonly string _streamingAssetsPath = Application.streamingAssetsPath;

        public List<GameConfig> AllConfigs => _allConfigs;
        public GameConfig CurrentConfig => _currentConfig;
        public bool IsDefault => _currentConfig.Equals(_allConfigs[0]);


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
            }

            foreach (var directoryInfo in modsFoldersDirectoryInfo.GetDirectories())
            {
                foreach (var fileInfo in directoryInfo.GetFiles("*.json"))
                {
                    using (StreamReader reader = new StreamReader(fileInfo.FullName))
                    {
                        var json = reader.ReadToEnd();
                        var config = JsonConvert.DeserializeObject<GameConfig>(json);

                        config.ModIconPath = GetFilePath(config.ModIconPath, _allConfigs[0].ModIconPath, config.ModName);
                        config.ContainerImagePath = GetFilePath(config.ContainerImagePath, _allConfigs[0].ContainerImagePath, config.ModName);

                        for (int i = 0; i < config.SuikaSkinsImagesPaths.Length; i++)
                        {
                            config.SuikaSkinsImagesPaths[i] = GetFilePath(config.SuikaSkinsImagesPaths[i], _allConfigs[0].SuikaSkinsImagesPaths[i], config.ModName);
                        }

                        for (int i = 0; i < config.SuikaIconsPaths.Length; i++)
                        {
                            config.SuikaIconsPaths[i] = GetFilePath(config.SuikaIconsPaths[i], _allConfigs[0].SuikaIconsPaths[i], config.ModName);
                        }

                        for (int i = 0; i < config.SuikaAudioPaths.Length; i++)
                        {
                            config.SuikaAudioPaths[i] = GetFilePath(config.SuikaAudioPaths[i], _allConfigs[0].SuikaAudioPaths[i], config.ModName);
                        }
                        
                        if (config.SuikaDropChances.Length < _allConfigs[0].SuikaDropChances.Length)
                        {
                            var originalLength = config.SuikaDropChances.Length;
                            Array.Resize(ref config.SuikaDropChances, _allConfigs[0].SuikaDropChances.Length);
    
                            for (int i = originalLength; i < config.SuikaDropChances.Length; i++)
                            {
                                config.SuikaDropChances[i] = _allConfigs[0].SuikaDropChances[i];
                                Debug.LogError($"Mod: {config.ModName} Missing drop chance at index {i}. Setting from default config. Value: {_allConfigs[0].SuikaDropChances[i]}");
                            }
                        }
                        else
                        {
                            Debug.LogError($"Mod: {config.ModName} have {config.SuikaDropChances.Length} drop chances. But should have {_allConfigs[0].SuikaDropChances.Length}. Trimming");
                            config.SuikaDropChances = config.SuikaDropChances.Take(_allConfigs[0].SuikaDropChances.Length).ToArray();
                        }

                        if (config.TimeBeforeTimerTrigger < 0)
                        {
                            Debug.LogError($"Mod: {config.ModName} TimeBeforeTimerTrigger is less than 0. Setting from default config. Value: {_allConfigs[0].TimeBeforeTimerTrigger}");
                        }
                
                        if (config.TimerStartTime < 0)
                        {
                            Debug.LogError($"Mod: {config.ModName} TimerStartTime is less than 0. Setting from default config. Value: {_allConfigs[0].TimeBeforeTimerTrigger}");
                        }

                        _allConfigs.Add(config);
                    }
                }
            }

            _currentConfig = _allConfigs[0];
        }

        private string GetFilePath(string filePath, string defaultPath, string modName)
        {
            var fullPath = Path.Combine(_modsPath, filePath);
            if (!File.Exists(fullPath))
            {
                Debug.LogError($"Mod: {modName} File not found. Setting from default config path: {fullPath}");
                return defaultPath;
            }
            return fullPath;
        }

        public void SetCurrentConfig(int index) => _currentConfig = _allConfigs[index];
    }
}