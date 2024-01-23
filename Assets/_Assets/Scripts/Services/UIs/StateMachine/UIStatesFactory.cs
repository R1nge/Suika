using _Assets.Scripts.Services.UIs.StateMachine.States;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStatesFactory
    {
        private readonly UIFactory _uiFactory;

        private UIStatesFactory(UIFactory uiFactory) => _uiFactory = uiFactory;

        public IUIState CreateLoadingState(UIStateMachine stateMachine) => new LoadingState(_uiFactory);

        public IUIState CreateMainMenuState(UIStateMachine stateMachine) => new MainMenuState(_uiFactory);

        public IUIState CreateGameState(UIStateMachine stateMachine) => new GameState(_uiFactory);

        public IUIState CreateGameOverState(UIStateMachine stateMachine) => new GameOverState(_uiFactory);

        public IUIState CreateModsState(UIStateMachine stateMachine) => new ModsState(_uiFactory);

        public IUIState CreateSettingsState(UIStateMachine stateMachine) => new SettingsState(_uiFactory);

        public IUIState CreateGamePauseState(UIStateMachine stateMachine) => new PauseState(_uiFactory);
    }
}