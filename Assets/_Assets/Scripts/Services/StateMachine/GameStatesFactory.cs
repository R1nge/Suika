using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.StateMachine.States;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStatesFactory
    {
        private readonly IDataService _dataService;
        private readonly UIStateMachine _uiStateMachine;
        private readonly CoroutineRunner _coroutineRunner;

        private GameStatesFactory(IDataService dataService, UIStateMachine uiStateMachine, CoroutineRunner coroutineRunner)
        {
            _dataService = dataService;
            _uiStateMachine = uiStateMachine;
            _coroutineRunner = coroutineRunner;
        }

        public IGameState CreateLoadSaveDataState(GameStateMachine stateMachine)
        {
            return new LoadSaveDataState(stateMachine, _dataService, _uiStateMachine, _coroutineRunner);
        }

        public IGameState CreateGameState(GameStateMachine stateMachine)
        {
            return new GameState(stateMachine);
        }

        public IGameState CreateGameOverState(GameStateMachine stateMachine)
        {
            return new GameOverState(_dataService);
        }

        public IGameState CreateSaveDataState(GameStateMachine stateMachine)
        {
            return new SaveDataState();
        }
    }
}