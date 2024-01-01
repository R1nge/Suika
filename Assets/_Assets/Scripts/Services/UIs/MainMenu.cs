using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button modsButton;
        [SerializeField] private Button quitButton;
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private UIStateMachine _uiStateMachine;

        private void Awake()
        {
            playButton.onClick.AddListener(Play);
            modsButton.onClick.AddListener(Mods);
            quitButton.onClick.AddListener(Quit);
        }

        private void Play() => _gameStateMachine.SwitchState(GameStateType.Game);

        private void Mods() => _uiStateMachine.SwitchState(UIStateType.Mods);

        private void Quit() => Application.Quit();
    }
}