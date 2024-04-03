using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.GameModes;
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
        private readonly GameModeService _gameModeService;
        private readonly TimeRushTimer _timeRushTimer;

        public ContinueGameState(UIStateMachine uiStateMachine, ContainerFactory containerFactory, PlayerFactory playerFactory, PlayerInput playerInput, ContinueGameService continueGameService, GameModeService gameModeService, TimeRushTimer timeRushTimer)
        {
            _uiStateMachine = uiStateMachine;
            _containerFactory = containerFactory;
            _playerFactory = playerFactory;
            _playerInput = playerInput;
            _continueGameService = continueGameService;
            _gameModeService = gameModeService;
            _timeRushTimer = timeRushTimer;
        }

        public async UniTask Enter()
        {
            // await _uiStateMachine.SwitchState(UIStateType.Loading);
            // _continueGameService.Continue();
            // _containerFactory.Create();
            // var player = await _playerFactory.Create();
            // await _uiStateMachine.SwitchState(UIStateType.Game);
            // _continueGameService.UpdateScore();
            // player.GetComponent<PlayerController>().SpawnContinue();
            // _playerInput.Enable();
            //
            // if (_gameModeService.SelectedGameMode == GameModeService.GameMode.TimeRush)
            // {
            //     _timeRushTimer.Resume();
            // }
        }

        public void Exit()
        {
        }
    }
}