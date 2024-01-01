using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace _Assets.Scripts.Services.Datas.GameConfigs
{
    public class GameConfigLoaderJson : IConfigLoader
    {
        private GameConfig _currentConfig;
        private List<GameConfig> _allConfigs;
        private readonly string _path = $"{Application.dataPath}\\Mods";

        public List<GameConfig> AllConfigs => _allConfigs;
        public GameConfig CurrentConfig => _currentConfig;

        public void LoadAllConfigs()
        {
            Debug.LogError($"Path: {_path}");

            var modsFoldersDirectoryInfo = new DirectoryInfo(_path);

            if (!modsFoldersDirectoryInfo.Exists)
            {
                modsFoldersDirectoryInfo.Create();
            }

            var folders = modsFoldersDirectoryInfo.GetDirectories();
            Debug.LogError($"Folders Count: {folders.Length}");

            _allConfigs = new List<GameConfig>(folders.Length);

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
                        var modIconRelativePath = Path.Combine(_path, config.ModIconPath);
                        config.ModIconPath = modIconRelativePath;
                        var containerImageRelativePath = Path.Combine(_path, config.ContainerImagePath);
                        config.ContainerImagePath = containerImageRelativePath;
                        
                        for (int i = 0; i < config.SuikaSkinsImagesPaths.Length; i++)
                        {
                            var skinImageRelativePath = Path.Combine(_path, config.SuikaSkinsImagesPaths[i]);
                            config.SuikaSkinsImagesPaths[i] = skinImageRelativePath;
                        }

                        for (int i = 0; i < config.SuikaIconsPaths.Length; i++)
                        {
                            var suikaIconsRelativePath = Path.Combine(_path, config.SuikaIconsPaths[i]);
                            config.SuikaIconsPaths[i] = suikaIconsRelativePath;
                        }

                        for (int i = 0; i < config.SuikaAudioPaths.Length; i++)
                        {
                            var suikaAudioRelativePath = Path.Combine(_path, config.SuikaAudioPaths[i]);
                            config.SuikaSkinsImagesPaths[i] = suikaAudioRelativePath;
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