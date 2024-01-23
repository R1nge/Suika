using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class InGamePause : MonoBehaviour
    {
        [SerializeField] private Button pause;
        [SerializeField] private InputActionAsset controls;
        [Inject] private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            pause.onClick.AddListener(Pause);
            controls.FindActionMap("Game").FindAction("Pause").performed += PauseInputCallback;
        }

        private void PauseInputCallback(InputAction.CallbackContext context) => Pause();

        private void Pause() => _gameStateMachine.SwitchState(GameStateType.GamePause);

        private void OnDestroy()
        {
            pause.onClick.RemoveAllListeners();
            controls.FindActionMap("Game").FindAction("Pause").performed -= PauseInputCallback;
        }
    }
}