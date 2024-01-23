using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class InGamePauseMenu : MonoBehaviour
    {
        [SerializeField] private Slider soundSlider, musicSlider;
        [SerializeField] private Button backButton, mainMenuButton;
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private IAudioSettingsLoader _audioSettingsLoader;
        [Inject] private AudioService _audioService;
        [Inject] private PlayerInput _playerInput;

        private void Awake()
        {
            soundSlider.onValueChanged.AddListener(ToggleSound);
            musicSlider.onValueChanged.AddListener(ToggleMusic);
            mainMenuButton.onClick.AddListener(MainMenu);
            backButton.onClick.AddListener(Back);
        }

        private void Resume(InputAction.CallbackContext callback) => Back();

        private void Start()
        {
            _playerInput.OnPause += Resume;
            soundSlider.value = _audioSettingsLoader.AudioData.VFXVolume;
            musicSlider.value = _audioSettingsLoader.AudioData.MusicVolume;
        }

        private void MainMenu() => _gameStateMachine.SwitchState(GameStateType.ResetAndMainMenu);

        private void ToggleSound(float volume) => _audioService.ChangeSoundVolume(volume);

        private void ToggleMusic(float volume) => _audioService.ChangeMusicVolume(volume);

        private void Back() => _gameStateMachine.SwitchState(GameStateType.GameResume);

        private void OnDestroy()
        {
            soundSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.RemoveAllListeners();
            backButton.onClick.RemoveAllListeners();
            _playerInput.OnPause -= Resume;
        }
    }
}