namespace _Assets.Scripts.Services.Audio
{
    public struct AudioSettingsData
    {
        public bool IsSoundEnabled;
        public bool IsMusicEnabled;

        public AudioSettingsData(bool isSoundEnabled, bool isMusicEnabled)
        {
            IsSoundEnabled = isSoundEnabled;
            IsMusicEnabled = isMusicEnabled;
        }
    }
}