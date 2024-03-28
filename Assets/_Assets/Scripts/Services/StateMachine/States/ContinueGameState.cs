using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class ContinueGameState : IGameState
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly ContainerFactory _containerFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly PlayerInput _playerInput;
        private readonly ContinueGameService _continueGameService;

        public ContinueGameState(UIStateMachine uiStateMachine, ContainerFactory containerFactory, PlayerFactory playerFactory, PlayerInput playerInput, ContinueGameService continueGameService)
        {
            _uiStateMachine = uiStateMachine;
            _containerFactory = containerFactory;
            _playerFactory = playerFactory;
            _playerInput = playerInput;
            _continueGameService = continueGameService;
        }

        public async UniTask Enter()
        {
            await _uiStateMachine.SwitchState(UIStateType.Loading);
            _continueGameService.Continue();
            _containerFactory.Create();
            var player = await _playerFactory.Create();
            await _uiStateMachine.SwitchState(UIStateType.Game);
            _continueGameService.UpdateScore();
            player.GetComponent<PlayerDrop>().SpawnContinue();
            _playerInput.Enable();
        }

        public void Exit()
        {
        }
    }
}