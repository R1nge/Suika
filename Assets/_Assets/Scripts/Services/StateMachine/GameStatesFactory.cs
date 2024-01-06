﻿using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.Datas.Mods;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.StateMachine.States;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStatesFactory
    {
        private readonly IPlayerDataLoader _playerDataLoader;
        private readonly UIStateMachine _uiStateMachine;
        private readonly CoroutineRunner _coroutineRunner;
        private readonly PlayerFactory _playerFactory;
        private readonly ContainerFactory _containerFactory;
        private readonly ResetService _resetService;
        private readonly PlayerInput _playerInput;
        private readonly IConfigLoader _configLoader;
        private readonly AudioService _audioService;
        private readonly IModDataLoader _modDataLoader;

        private GameStatesFactory(IPlayerDataLoader playerDataLoader, UIStateMachine uiStateMachine, CoroutineRunner coroutineRunner, PlayerFactory playerFactory, ContainerFactory containerFactory, ResetService resetService, PlayerInput playerInput, IConfigLoader configLoader, AudioService audioService, IModDataLoader modDataLoader)
        {
            _playerDataLoader = playerDataLoader;
            _uiStateMachine = uiStateMachine;
            _coroutineRunner = coroutineRunner;
            _playerFactory = playerFactory;
            _containerFactory = containerFactory;
            _resetService = resetService;
            _playerInput = playerInput;
            _configLoader = configLoader;
            _audioService = audioService;
            _modDataLoader = modDataLoader;
        }

        public IGameState CreateLoadSaveDataState(GameStateMachine stateMachine)
        {
            return new LoadSaveDataState(stateMachine, _playerDataLoader, _uiStateMachine, _configLoader, _modDataLoader);
        }

        public IGameState CreateGameState(GameStateMachine stateMachine)
        {
            return new GameState(stateMachine, _uiStateMachine, _playerFactory, _containerFactory, _playerInput, _audioService);
        }

        public IGameState CreateGameOverState(GameStateMachine stateMachine)
        {
            return new GameOverState(stateMachine, _uiStateMachine, _playerInput);
        }

        public IGameState CreateSaveDataState(GameStateMachine stateMachine)
        {
            return new SaveDataState(_playerDataLoader);
        }

        public IGameState CreateResetAndRetryState(GameStateMachine stateMachine)
        {
            return new ResetAndRetry(stateMachine, _resetService);
        }
        
        public IGameState CreateResetAndMainMenuState(GameStateMachine stateMachine)
        {
            return new ResetAndMainMenu(stateMachine, _resetService, _uiStateMachine);
        }
    }
}