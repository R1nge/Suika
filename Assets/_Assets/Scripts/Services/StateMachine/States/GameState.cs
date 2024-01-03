using System;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly PlayerFactory _playerFactory;
        private readonly ContainerFactory _containerFactory;
        private readonly PlayerInput _playerInput;
        private readonly AudioService _audioService;

        public GameState(GameStateMachine stateMachine, UIStateMachine uiStateMachine, PlayerFactory playerFactory,
            ContainerFactory containerFactory, PlayerInput playerInput, AudioService audioService)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _playerFactory = playerFactory;
            _containerFactory = containerFactory;
            _playerInput = playerInput;
            _audioService = audioService;
        }

        public async void Enter()
        {
            _uiStateMachine.SwitchState(UIStateType.Loading);
            var player = _playerFactory.Create().GetComponent<PlayerDrop>();
            _playerInput.Enable();
            await _containerFactory.Create();
            await _audioService.PlaySong(0);
            await UniTask.Delay(500);
            _uiStateMachine.SwitchState(UIStateType.Game);
            player.SpawnSuika();
        }

        public void Exit()
        {
        }
    }
}