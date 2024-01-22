namespace _Assets.Scripts.Services.Vibrations
{
    public class VibrationService
    {
        private readonly IVibrationSettingLoader _vibrationSettingLoader;

        private VibrationService(IVibrationSettingLoader vibrationSettingLoader)
        {
            _vibrationSettingLoader = vibrationSettingLoader;
        }

        public void Init()
        {
#if UNITY_ANDROID
            Vibration.Init();
#endif
        }

        public void Vibrate()
        {
#if UNITY_ANDROID
            if (_vibrationSettingLoader.VibrationSettingsData.Enabled)
            {
                Vibration.Vibrate();
            }
#endif
        }
    }
}