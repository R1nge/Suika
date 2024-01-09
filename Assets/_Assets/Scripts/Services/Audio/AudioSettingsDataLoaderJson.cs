using System.IO;
using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Assets.Scripts.Services.Audio
{
    public class AudioSettingsDataLoaderJson : IAudioSettingsLoader
    {
        private AudioSettingsData _audioSettingsData;
        public AudioSettingsData AudioData => _audioSettingsData;
        public void ToggleSound(bool enable) => _audioSettingsData.IsSoundEnabled = enable;

        public void ToggleMusic(bool enable) => _audioSettingsData.IsMusicEnabled = enable;

        public async UniTask Load()
        {
            var dataFolderInfo = new DirectoryInfo(PathsHelper.DataPath);
            
            if(!File.Exists(Path.Combine(PathsHelper.DataPath, PathsHelper.SettingsDataJson)))
            {
                Debug.LogWarning("Audio settings not found");
                return;
            }

            foreach (var fileInfo in dataFolderInfo.GetFiles(PathsHelper.SettingsDataJson))
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
            var path = Path.Combine(PathsHelper.DataPath, PathsHelper.SettingsDataJson);
            var json = JsonConvert.SerializeObject(_audioSettingsData);
            File.WriteAllText(path, json);
        }
    }
}