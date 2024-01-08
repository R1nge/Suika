using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
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
        [SerializeField] private Image background;
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private UIStateMachine _uiStateMachine;

        public void Init(Sprite sprite) => background.sprite = sprite;

        public void Show() => gameObject.SetActive(true);

        private void Awake()
        {
            playButton.onClick.AddListener(Play);
            modsButton.onClick.AddListener(Mods);
            quitButton.onClick.AddListener(Quit);
        }

        private void Play() => _gameStateMachine.SwitchState(GameStateType.Game);

        private void Mods() => _uiStateMachine.SwitchState(UIStateType.Mods).Forget();

        private void Quit() => Application.Quit();
    }
}