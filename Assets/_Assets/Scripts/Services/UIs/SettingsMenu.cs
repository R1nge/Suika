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

        private void Awake()
        {
            soundToggle.onValueChanged.AddListener(ToggleSound);
            musicToggle.onValueChanged.AddListener(ToggleMusic);
            backButton.onClick.AddListener(Back);
        }

        public void ToggleSound(bool enable)
        {
            
        }

        public void ToggleMusic(bool enable)
        {
            
        }

        private void Back() => _uiStateMachine.SwitchToPreviousState().Forget();
    }
}