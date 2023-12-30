using _Assets.Scripts.Services.UIs.StateMachine.States;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStatesFactory
    {
        public IUIState CreateLoadingState(UIStateMachine stateMachine)
        {
            return new LoadingState();
        }
        
        public IUIState CreateMainMenuState(UIStateMachine stateMachine)
        {
            return new MainMenuState();
        }
        
        public IUIState CreateGameState(UIStateMachine stateMachine)
        {
            return new GameState();
        }
        
        public IUIState CreateGameOverState(UIStateMachine stateMachine)
        {
            return new GameOverState();
            
        }
    }
}