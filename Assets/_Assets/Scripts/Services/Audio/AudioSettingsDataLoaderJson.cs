using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Assets.Scripts.Services.Audio
{
    public class AudioSettingsDataLoaderJson : IAudioSettingsLoader
    {
        private AudioSettingsData _audioSettingsData;
        public AudioSettingsData AudioData => _audioSettingsData;
        public void ToggleSound(bool enable)
        {
            _audioSettingsData.IsSoundEnabled = enable;
            Debug.LogError($"Is sound enabled {_audioSettingsData.IsSoundEnabled}");
        }

        public void ToggleMusic(bool enable) => _audioSettingsData.IsMusicEnabled = enable;

        private readonly string _path = Path.Combine(Application.persistentDataPath, "Data");

        public async UniTask Load()
        {
            var dataFolderInfo = new DirectoryInfo(_path);
            
            if(!File.Exists(Path.Combine(_path, "settingsData.json")))
            {
                Debug.LogWarning("Audio settings not found");
                return;
            }

            foreach (var fileInfo in dataFolderInfo.GetFiles("settingsData.json"))
            {
                StreamReader reader = new StreamReader(fileInfo.FullName);
                var json = await reader.ReadToEndAsync();
                _audioSettingsData = JsonConvert.DeserializeObject<AudioSettingsData>(json);
                reader.Close();
                reader.Dispose();
            }
        }

        public void Save()
        {
            var path = Path.Combine(_path, "settingsData.json");
            var json = JsonConvert.SerializeObject(_audioSettingsData);
            File.WriteAllText(path, json);
        }
    }
}