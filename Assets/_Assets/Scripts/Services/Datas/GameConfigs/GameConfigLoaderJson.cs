using System.Collections.Generic;
using System.IO;
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


        public async void LoadDefaultConfig()
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
                
                Debug.LogError($"DEFAULT CONFIG: {json}");

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

                _allConfigs.Add(config);
            }
        }

        public void LoadAllConfigs()
        {
            Debug.LogError($"Path: {_modsPath}");

            var modsFoldersDirectoryInfo = new DirectoryInfo(_modsPath);

            if (!modsFoldersDirectoryInfo.Exists)
            {
                modsFoldersDirectoryInfo.Create();
            }

            var folders = modsFoldersDirectoryInfo.GetDirectories();
            Debug.LogError($"Folders Count: {folders.Length}");

            foreach (var directoryInfo in folders)
            {
                Debug.LogError($"Folder Name: {directoryInfo.Name}");

                var files = directoryInfo.GetFiles("*.json");

                foreach (var fileInfo in files)
                {
                    Debug.LogError($"File Name: {fileInfo.Name}");

                    using (StreamReader reader = new StreamReader(fileInfo.FullName))
                    {
                        var json = reader.ReadToEnd();
                        var config = JsonConvert.DeserializeObject<GameConfig>(json);

                        var modIconRelativePath = Path.Combine(_modsPath, config.ModIconPath);
                        config.ModIconPath = modIconRelativePath;

                        var containerImageRelativePath = Path.Combine(_modsPath, config.ContainerImagePath);
                        config.ContainerImagePath = containerImageRelativePath;

                        for (int i = 0; i < config.SuikaSkinsImagesPaths.Length; i++)
                        {
                            var skinImageRelativePath = Path.Combine(_modsPath, config.SuikaSkinsImagesPaths[i]);
                            config.SuikaSkinsImagesPaths[i] = skinImageRelativePath;
                        }

                        for (int i = 0; i < config.SuikaIconsPaths.Length; i++)
                        {
                            var suikaIconsRelativePath = Path.Combine(_modsPath, config.SuikaIconsPaths[i]);
                            config.SuikaIconsPaths[i] = suikaIconsRelativePath;
                        }

                        for (int i = 0; i < config.SuikaAudioPaths.Length; i++)
                        {
                            var suikaAudioRelativePath = Path.Combine(_modsPath, config.SuikaAudioPaths[i]);
                            config.SuikaAudioPaths[i] = suikaAudioRelativePath;
                        }

                        _allConfigs.Add(config);
                    }
                }
            }

            _currentConfig = _allConfigs[0];
        }

        public void SetCurrentConfig(int index) => _currentConfig = _allConfigs[index];
    }
}