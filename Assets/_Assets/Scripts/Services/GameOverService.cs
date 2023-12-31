using System;
using _Assets.Scripts.Services.StateMachine;
using VContainer.Unity;

namespace _Assets.Scripts.Services
{
    public class GameOverService : IInitializable, IDisposable
    {
        private readonly GameOverTimer _gameOverTimer;
        private readonly GameStateMachine _gameStateMachine;

        public GameOverService(GameOverTimer gameOverTimer, GameStateMachine gameStateMachine)
        {
            _gameOverTimer = gameOverTimer;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize() => _gameOverTimer.OnTimerEnded += GameOver;

        private void GameOver() => _gameStateMachine.SwitchState(GameStateType.GameOver);

        public void Dispose() => _gameOverTimer.OnTimerEnded -= GameOver;
    }
}