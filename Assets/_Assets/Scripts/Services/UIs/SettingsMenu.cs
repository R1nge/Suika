using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Vibrations;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Slider soundSlider, musicSlider;
        [SerializeField] private Button vibrationButton;
        [SerializeField] private Image vibrationButtonImage;
        [SerializeField] private Button backButton;
        [SerializeField] private Image background;
        [Inject] private UIStateMachine _uiStateMachine;
        [Inject] private IAudioSettingsLoader _audioSettingsLoader;
        [Inject] private AudioService _audioService;
        [Inject] private IVibrationSettingLoader _vibrationSettingLoader;

        public void Init(Sprite sprite) => background.sprite = sprite;

        private void Awake()
        {
            soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
            musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
            vibrationButton.onClick.AddListener(ToggleVibration);
            backButton.onClick.AddListener(Back);
        }

        private void Start()
        {
            soundSlider.value = _audioSettingsLoader.AudioData.VFXVolume;
            musicSlider.value = _audioSettingsLoader.AudioData.MusicVolume;
            ChangeButton();
        }

        private void ChangeSoundVolume(float volume) => _audioService.ChangeSoundVolume(volume);

        private void ChangeMusicVolume(float volume) => _audioService.ChangeMusicVolume(volume);

        private void ToggleVibration()
        {
            _vibrationSettingLoader.Toggle(!_vibrationSettingLoader.VibrationSettingsData.Enabled);
            ChangeButton();
        }

        private void ChangeButton()
        {
            vibrationButtonImage.color = _vibrationSettingLoader.VibrationSettingsData.Enabled ? Color.green : Color.red;
        }

        private async void Back() => await _uiStateMachine.SwitchToPreviousState();

        private void OnDestroy()
        {
            soundSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.RemoveAllListeners();
            vibrationButton.onClick.RemoveAllListeners();
            backButton.onClick.RemoveAllListeners();
        }
    }
}