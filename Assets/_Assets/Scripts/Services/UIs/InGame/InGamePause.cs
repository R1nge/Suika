using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class InGamePause : MonoBehaviour
    {
        [SerializeField] private Button pause;
        [Inject] private GameStateMachine _gameStateMachine;

        private void Awake() => pause.onClick.AddListener(Pause);

        private void Pause() => _gameStateMachine.SwitchState(GameStateType.GamePause);

        private void OnDestroy() => pause.onClick.RemoveAllListeners();
    }
}