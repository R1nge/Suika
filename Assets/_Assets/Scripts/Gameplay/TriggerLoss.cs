﻿using _Assets.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class TriggerLoss : MonoBehaviour
    {
        private const float TriggerThreshold = 1f;
        private float _time;
        [Inject] private GameOverTimer _gameOverTimer;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Suika suika))
            {
                if (suika.IsLanded)
                {
                    _time += Time.deltaTime;

                    if (_time >= TriggerThreshold)
                    {
                        _gameOverTimer.StartTimer();
                    }
                }
                else
                {
                    _time = 0;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Suika suika))
            {
                _gameOverTimer.StopTimer();
            }
        }
    }
}