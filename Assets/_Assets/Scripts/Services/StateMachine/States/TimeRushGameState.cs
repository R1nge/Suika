using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class TimeRushGameState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly PlayerInput _playerInput;
        private readonly ContainerFactory _containerFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly TimeRushTimer _timeRushTimer;

        public TimeRushGameState(GameStateMachine gameStateMachine, UIStateMachine uiStateMachine,
            PlayerInput playerInput, ContainerFactory containerFactory, PlayerFactory playerFactory, TimeRushTimer timeRushTimer)
        {
            _gameStateMachine = gameStateMachine;
            _uiStateMachine = uiStateMachine;
            _playerInput = playerInput;
            _containerFactory = containerFactory;
            _playerFactory = playerFactory;
            _timeRushTimer = timeRushTimer;
        }

        public async UniTask Enter()
        {
            await _uiStateMachine.SwitchState(UIStateType.Loading);
            _containerFactory.Create();
            var player = await _playerFactory.Create();
            await _uiStateMachine.SwitchState(UIStateType.Game);
            player.GetComponent<PlayerController>().SpawnSuika();
            _playerInput.Enable();
            _timeRushTimer.Start();
        }

        public void Exit()
        {
            _timeRushTimer.Stop();
        }
    }
}