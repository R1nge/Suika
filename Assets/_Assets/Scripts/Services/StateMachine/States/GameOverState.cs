using System;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameOverState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly UIStateMachine _uiStateMachine;

        public GameOverState(GameStateMachine gameStateMachine, UIStateMachine uiStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _uiStateMachine = uiStateMachine;
        }

        public void Enter()
        {
            _uiStateMachine.SwitchState(UIStateType.GameOver);
            _gameStateMachine.SwitchState(GameStateType.SaveData);
            Debug.LogError("GAME OVER");
        }

        public void Exit()
        {
        }
    }
}