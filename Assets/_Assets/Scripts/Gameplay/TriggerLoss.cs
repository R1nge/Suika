﻿using System;
using System.Collections.Generic;
using _Assets.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class TriggerLoss : MonoBehaviour
    {
        private const float TriggerThreshold = 1.5f;
        private float _time;
        private List<Suika> _collidedSuikas = new(10);
        [Inject] private GameOverTimer _gameOverTimer;

        private void Update()
        {
            if (_collidedSuikas.Count > 0)
            {
                _time += Time.deltaTime;

                if (_time >= TriggerThreshold)
                {
                    _gameOverTimer.StartTimer();
                }
            }
            else
            {
                _gameOverTimer.StopTimer();
                _time = 0;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Suika suika))
            {
                if (!_collidedSuikas.Contains(suika))
                {
                    if (suika.HasLanded)
                    {
                        _collidedSuikas.Add(suika);
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Suika suika))
            {
                if (_collidedSuikas.Contains(suika))
                {
                    _collidedSuikas.Remove(suika);
                }
            }
        }
    }
}