using System;
using UnityEngine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameOverState : IGameState
    {

        public void Enter()
        {
            //TODO: Show Game Over (Retry, main menu)
            Debug.LogError("GAME OVER");
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}