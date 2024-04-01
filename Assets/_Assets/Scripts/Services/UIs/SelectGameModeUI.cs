using _Assets.Scripts.Services.GameModes;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class SelectGameModeUI : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Button classicButton;
        [SerializeField] private Button timeRushButton;
        [SerializeField] private Button backButton;
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private GameModeService _gameModeService;
        [Inject] private UIStateMachine _uiStateMachine;

        public void Init(Sprite sprite) => background.sprite = sprite;
        
        private void Start()
        {
            classicButton.onClick.AddListener(Classic);
            timeRushButton.onClick.AddListener(TimeRush);
            backButton.onClick.AddListener(Back);
        }

        private void Classic()
        {
            _gameModeService.SelectGameMode(GameModeService.GameMode.Classic);
            _gameStateMachine.SwitchState(GameStateType.Classic).Forget();
        }

        private void TimeRush()
        {
            _gameModeService.SelectGameMode(GameModeService.GameMode.TimeRush);
            _gameStateMachine.SwitchState(GameStateType.TimeRush).Forget();
        }

        private void Back() => _uiStateMachine.SwitchState(UIStateType.MainMenu).Forget();

        private void OnDestroy()
        {
            classicButton.onClick.RemoveListener(Classic);
            timeRushButton.onClick.RemoveListener(TimeRush);
            backButton.onClick.RemoveListener(Back);
        }
    }
}