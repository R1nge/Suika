﻿using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;
        private readonly PlayerFactory _playerFactory;

        public GameState(GameStateMachine stateMachine, UIStateMachine uiStateMachine, PlayerFactory playerFactory)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
            _playerFactory = playerFactory;
        }

        public void Enter()
        {
            _uiStateMachine.SwitchState(UIStateType.Game);
            _playerFactory.Create();
        }

        public void Exit()
        {
        }
    }
}