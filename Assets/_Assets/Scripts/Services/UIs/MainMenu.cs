using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;
        [Inject] private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            playButton.onClick.AddListener(Play);
            quitButton.onClick.AddListener(Quit);
        }

        private void Play()
        {
            _gameStateMachine.SwitchState(GameStateType.Game);
        }

        private void Quit()
        {
            _gameStateMachine.SwitchState(GameStateType.SaveData);
            Application.Quit();
        }
    }
}