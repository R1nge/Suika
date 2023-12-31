using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.StateMachine.States;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStatesFactory
    {
        private readonly IDataService _dataService;
        private readonly UIStateMachine _uiStateMachine;
        private readonly CoroutineRunner _coroutineRunner;
        private readonly PlayerFactory _playerFactory;
        private readonly ContainerFactory _containerFactory;
        private readonly ResetService _resetService;

        private GameStatesFactory(IDataService dataService, UIStateMachine uiStateMachine, CoroutineRunner coroutineRunner, PlayerFactory playerFactory, ContainerFactory containerFactory, ResetService resetService)
        {
            _dataService = dataService;
            _uiStateMachine = uiStateMachine;
            _coroutineRunner = coroutineRunner;
            _playerFactory = playerFactory;
            _containerFactory = containerFactory;
            _resetService = resetService;
        }

        public IGameState CreateLoadSaveDataState(GameStateMachine stateMachine)
        {
            return new LoadSaveDataState(stateMachine, _dataService, _uiStateMachine, _coroutineRunner);
        }

        public IGameState CreateGameState(GameStateMachine stateMachine)
        {
            return new GameState(stateMachine, _uiStateMachine, _playerFactory, _containerFactory);
        }

        public IGameState CreateGameOverState(GameStateMachine stateMachine)
        {
            return new GameOverState(stateMachine, _uiStateMachine);
        }

        public IGameState CreateSaveDataState(GameStateMachine stateMachine)
        {
            return new SaveDataState(_dataService);
        }

        public IGameState CreateResetAndRetryState(GameStateMachine stateMachine)
        {
            return new ResetAndRetry(stateMachine, _resetService);
        }
    }
}