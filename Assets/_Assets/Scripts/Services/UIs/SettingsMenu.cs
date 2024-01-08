using System;
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
        [SerializeField] private Toggle soundToggle, musicToggle;
        [SerializeField] private Button backButton;
        [Inject] private UIStateMachine _uiStateMachine;
        [Inject] private IAudioSettingsLoader _audioSettingsLoader;

        private void Awake()
        {
            soundToggle.onValueChanged.AddListener(ToggleSound);
            musicToggle.onValueChanged.AddListener(ToggleMusic);
            backButton.onClick.AddListener(Back);
        }

        private void Start()
        {
            Debug.LogError("Sound enabled: " + _audioSettingsLoader.AudioData.IsSoundEnabled);
            soundToggle.isOn = _audioSettingsLoader.AudioData.IsSoundEnabled;
            musicToggle.isOn = _audioSettingsLoader.AudioData.IsMusicEnabled;
        }

        private void ToggleSound(bool enable)
        {
            Debug.LogError("TOGGLE Sound" + enable);
            _audioSettingsLoader.ToggleSound(enable);
        }

        private void ToggleMusic(bool enable) => _audioSettingsLoader.ToggleMusic(enable);

        private void Back()
        {
            _audioSettingsLoader.Save();
            _uiStateMachine.SwitchToPreviousState().Forget();
        }

        private void OnDestroy()
        {
            soundToggle.onValueChanged.RemoveAllListeners();
            musicToggle.onValueChanged.RemoveAllListeners();
            backButton.onClick.RemoveAllListeners();
        }
    }
}