namespace _Assets.Scripts.Services.Audio
{
    public struct AudioSettingsData
    {
        public float VFXVolume;
        public float MusicVolume;

        public AudioSettingsData(float vfxVolume, float musicVolume)
        {
            VFXVolume = vfxVolume;
            MusicVolume = musicVolume;
        }
    }
}