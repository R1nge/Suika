namespace _Assets.Scripts.Services
{
    public class VibrationService
    {
        public void Init()
        {
#if UNITY_ANDROID
            Vibration.Init();
#endif
        }

        public void Vibrate()
        {
#if UNITY_ANDROID
            Vibration.Vibrate();
#endif
        }
    }
}