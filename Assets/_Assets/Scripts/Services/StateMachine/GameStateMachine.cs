﻿using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<GameStateType, IGameState> _states;
        private IGameState _currentGameState;
        private GameStateType _currentGameStateType;

        public GameStateMachine(GameStatesFactory gameStatesFactory)
        {
            _states = new Dictionary<GameStateType, IGameState>
            {
                { GameStateType.LoadSavedData, gameStatesFactory.CreateLoadSaveDataState(this) },
                { GameStateType.Game, gameStatesFactory.CreateGameState(this) },
                { GameStateType.GameOver, gameStatesFactory.CreateGameOverState(this) },
                { GameStateType.SaveData, gameStatesFactory.CreateSaveDataState(this) },
                { GameStateType.ResetAndRetry, gameStatesFactory.CreateResetAndRetryState(this) },
                { GameStateType.ResetAndMainMenu, gameStatesFactory.CreateResetAndMainMenuState(this) }
            };
        }

        public void SwitchState(GameStateType gameStateType)
        {
            if(_currentGameStateType == gameStateType)
            {
                Debug.LogError($"Already in {_currentGameStateType} state");
                return;
            }
            
            _currentGameState?.Exit();
            _currentGameState = _states[gameStateType];
            _currentGameStateType = gameStateType;
            _currentGameState.Enter();
        }
    }
}