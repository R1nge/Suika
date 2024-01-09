using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class InGamePauseMenu : MonoBehaviour
    {
        [SerializeField] private Toggle soundToggle, musicToggle;
        [SerializeField] private Button backButton, mainMenuButton;
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private IAudioSettingsLoader _audioSettingsLoader;
        [Inject] private AudioService _audioService;
        [Inject] private UIStateMachine _uiStateMachine;

        private void Awake()
        {
            soundToggle.onValueChanged.AddListener(ToggleSound);
            musicToggle.onValueChanged.AddListener(ToggleMusic);
            mainMenuButton.onClick.AddListener(MainMenu);
            backButton.onClick.AddListener(Back);
        }

        private void MainMenu()
        {
            _gameStateMachine.SwitchState(GameStateType.ResetAndMainMenu);
        }

        private void Start()
        {
            soundToggle.isOn = _audioSettingsLoader.AudioData.IsSoundEnabled;
            musicToggle.isOn = _audioSettingsLoader.AudioData.IsMusicEnabled;
        }

        private void ToggleSound(bool enable) => _audioService.ToggleSound(enable);

        private void ToggleMusic(bool enable) => _audioService.ToggleMusic(enable);

        private void Back()
        {
            _audioSettingsLoader.Save();
            _gameStateMachine.SwitchState(GameStateType.GameResume);
        }

        private void OnDestroy()
        {
            soundToggle.onValueChanged.RemoveAllListeners();
            musicToggle.onValueChanged.RemoveAllListeners();
            backButton.onClick.RemoveAllListeners();
        }
    }
}