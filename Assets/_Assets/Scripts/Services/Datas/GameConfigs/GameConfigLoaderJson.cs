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
        private readonly string _path = $"{Application.dataPath}/Mods/";

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
                        Debug.LogError($"Json: {json}");
                        _allConfigs.Add(JsonConvert.DeserializeObject<GameConfig>(json));
                        
                    }
                }
            }

            _currentConfig = _allConfigs[0];
        }

        public void SetCurrentConfig(int index) => _currentConfig = _allConfigs[index];
    }
}