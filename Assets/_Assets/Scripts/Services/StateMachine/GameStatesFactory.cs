using _Assets.Scripts.Services.StateMachine.States;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStatesFactory
    {
        public IGameState CreateLoadSaveDataState(GameStateMachine stateMachine)
        {
            return new LoadSaveDataState(stateMachine);
        }

        public IGameState CreateGameState(GameStateMachine stateMachine)
        {
            return new GameState(stateMachine);
        }
    }
}