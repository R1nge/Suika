using System;
using _Assets.Scripts.Services.Datas.Mods;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Button modsButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Image background;
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private UIStateMachine _uiStateMachine;
        [Inject] private ContinueGameService _continueGameService;
        [Inject] private PlayerInput _playerInput;

        public void Init(Sprite sprite) => background.sprite = sprite;

        private void Awake()
        {
            playButton.onClick.AddListener(Play);
            modsButton.onClick.AddListener(Mods);
            settingsButton.onClick.AddListener(Settings);
            quitButton.onClick.AddListener(Quit);
        }

        private void Start()
        {
            _playerInput.OnDeviceChanged += SelectUIElement;

            var hasValidContinueData = _continueGameService.HasData;

            if (hasValidContinueData)
            {
                continueButton.onClick.AddListener(Continue);

                var playButtonNavigation = playButton.navigation;
                playButtonNavigation.selectOnUp = continueButton;
                playButtonNavigation.selectOnDown = modsButton;
                playButton.navigation = playButtonNavigation;

                var quitButtonNavigation = quitButton.navigation;
                quitButtonNavigation.selectOnUp = settingsButton;
                quitButtonNavigation.selectOnDown = continueButton;
                quitButton.navigation = quitButtonNavigation;
            }

            continueButton.gameObject.SetActive(hasValidContinueData);

            SelectFirstButton();
        }

        private void SelectFirstButton()
        {
            var hasValidContinueData = _continueGameService.HasData;

            if (hasValidContinueData)
            {
                EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(playButton.gameObject);
            }
        }

        private void Update()
        {
            //Could use InvokeRepeat
            SelectFirstUIElementIfNotSelected();
        }

        private void SelectFirstUIElementIfNotSelected()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                SelectFirstButton();
            }
        }

        private void SelectUIElement(InputDevice device)
        {
            if (device.name == "Gamepad")
            {
                SelectFirstButton();
            }
        }

        private void Continue() => _gameStateMachine.SwitchState(GameStateType.ContinueGame);

        private void Play() => _gameStateMachine.SwitchState(GameStateType.Game);

        private void Mods() => _uiStateMachine.SwitchStateAndExitFromAllPrevious(UIStateType.Mods).Forget();

        private void Settings() => _uiStateMachine.SwitchState(UIStateType.Settings).Forget();

        private void Quit() => Application.Quit();
    }
}