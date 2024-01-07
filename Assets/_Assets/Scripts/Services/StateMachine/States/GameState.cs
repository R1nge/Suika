﻿using _Assets.Scripts.Gameplay;
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
            await _uiStateMachine.SwitchState(UIStateType.Loading, 0, 100);
            await _containerFactory.Create();
            await _audioService.PlaySong(0);
            await _uiStateMachine.SwitchState(UIStateType.Game, 1000);
            var player = await _playerFactory.Create();
            _playerInput.Enable();
            player.GetComponent<PlayerDrop>().SpawnSuika();
        }

        public void Exit()
        {
        }
    }
}