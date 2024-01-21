using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Slider soundSlider, musicSlider;
        [SerializeField] private Button backButton;
        [SerializeField] private Image background;
        [Inject] private UIStateMachine _uiStateMachine;
        [Inject] private IAudioSettingsLoader _audioSettingsLoader;
        [Inject] private AudioService _audioService;

        public void Init(Sprite sprite) => background.sprite = sprite;

        private void Awake()
        {
            soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
            musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
            backButton.onClick.AddListener(Back);
        }

        private void Start()
        {
            soundSlider.value = _audioSettingsLoader.AudioData.VFXVolume;
            musicSlider.value = _audioSettingsLoader.AudioData.MusicVolume;
        }

        private void ChangeSoundVolume(float volume) => _audioService.ChangeSoundVolume(volume);

        private void ChangeMusicVolume(float volume) => _audioService.ChangeMusicVolume(volume);

        private void Back()
        {
            _audioSettingsLoader.Save();
            _uiStateMachine.SwitchToPreviousState().Forget();
        }

        private void OnDestroy()
        {
            soundSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.RemoveAllListeners();
            backButton.onClick.RemoveAllListeners();
        }
    }
}