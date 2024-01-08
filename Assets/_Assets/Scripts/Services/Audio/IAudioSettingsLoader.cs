using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.Audio
{
    public interface IAudioSettingsLoader
    {
        UniTask Load();
        void Save();
        AudioSettingsData AudioData { get;}
        void ToggleSound(bool enable);
        void ToggleMusic(bool enable);
    }
}