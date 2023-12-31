using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Button mainMenu;
        [SerializeField] private Button restart;
        [Inject] private GameStateMachine _stateMachine;

        private void Awake()
        {
            mainMenu.onClick.AddListener(ShowMainMenu);
            restart.onClick.AddListener(Restart);
        }

        private void ShowMainMenu()
        {
            _stateMachine.SwitchState(GameStateType.ResetAndMainMenu);
        }

        private void Restart()
        {
            _stateMachine.SwitchState(GameStateType.ResetAndRetry);
        }
    }
}