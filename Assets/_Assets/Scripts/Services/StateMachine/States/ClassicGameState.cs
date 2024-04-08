using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class ClassicGameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly PlayerFactory _playerFactory;
        private readonly ContainerFactory _containerFactory;
        private readonly PlayerInput _playerInput;

        public ClassicGameState(GameStateMachine stateMachine, UIStateMachine uiStateMachine, PlayerFactory playerFactory,
            ContainerFactory containerFactory, PlayerInput playerInput)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _playerFactory = playerFactory;
            _containerFactory = containerFactory;
            _playerInput = playerInput;
        }

        public async UniTask Enter()
        {
            await _uiStateMachine.SwitchState(UIStateType.Loading);
            _containerFactory.Create();
            var player = await _playerFactory.Create();
            player.GetComponent<PlayerController>().SpawnSuika();
            await _uiStateMachine.SwitchState(UIStateType.Game);
            _playerInput.Enable();
        }

        public void Exit()
        {
        }
    }
}