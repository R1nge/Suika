using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Vibrations;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class SettingsState : IUIState
    {
        private readonly UIFactory _uiFactory;
        private SettingsMenu _ui;
        private readonly IAudioSettingsLoader _audioSettingsLoader;
        private readonly IVibrationSettingLoader _vibrationSettingLoader;

        public SettingsState(UIFactory uiFactory, IAudioSettingsLoader audioSettingsLoader, IVibrationSettingLoader vibrationSettingLoader)
        {
            _uiFactory = uiFactory;
            _audioSettingsLoader = audioSettingsLoader;
            _vibrationSettingLoader = vibrationSettingLoader;
        }

        public async UniTask Enter()
        {
            _ui = _uiFactory.CreateSettingsMenu();
            await _ui.GetComponent<UICanvasAnimation>().Play(AnimationType.FadeIn);
        }

        public async UniTask Exit()
        {
            _audioSettingsLoader.Save();
            _vibrationSettingLoader.Save();
            await _ui.GetComponent<UICanvasAnimation>().Play(AnimationType.FadeOut);
            await UniTask.DelayFrame(1);
            Object.Destroy(_ui.gameObject);
        }
    }
}