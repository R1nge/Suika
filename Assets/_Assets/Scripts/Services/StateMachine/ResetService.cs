using System.Collections.Generic;
using _Assets.Scripts.Gameplay;
using UnityEngine;

namespace _Assets.Scripts.Services.StateMachine
{
    public class ResetService
    {
        private readonly List<Suika> _suikas = new();
        private GameObject _suikaContainer;
        private GameObject _player;
        private readonly GameOverTimer _gameOverTimer;

        private ResetService(GameOverTimer gameOverTimer)
        {
            _gameOverTimer = gameOverTimer;
        }

        public void AddSuika(Suika suika) => _suikas.Add(suika);
        public void RemoveSuika(Suika suika) => _suikas.Remove(suika);
        public void SetContainer(GameObject suikaContainer) => _suikaContainer = suikaContainer;
        public void SetPlayer(GameObject player) => _player = player;

        public void Reset()
        {
            _gameOverTimer.StopTimer();
            
            var suikasCount = _suikas.Count - 1;
            for (int i = suikasCount; i >= 0; i--)
            {
                Object.Destroy(_suikas[i].gameObject);
            }

            _suikas.Clear();

            Object.Destroy(_suikaContainer);
            Object.Destroy(_player);
        }
    }
}