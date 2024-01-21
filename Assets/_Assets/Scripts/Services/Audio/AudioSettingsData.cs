namespace _Assets.Scripts.Services.Audio
{
    public struct AudioSettingsData
    {
        public float VFXVolume;
        public float MusicVolume;

        public AudioSettingsData(int vfxVolume, int musicVolume)
        {
            VFXVolume = vfxVolume;
            MusicVolume = musicVolume;
        }
    }
}