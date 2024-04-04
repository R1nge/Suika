using System;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.Audio
{
    public interface IAudioSettingsLoader
    {
        UniTask Load();
        void Save();
        AudioSettingsData AudioData { get; }
        void ChangeSoundVolume(float volume);
        void ChangeMusicVolume(float volume);
        event Action<AudioSettingsData> OnDataChanged; 
    }
}