﻿using System;
using UnityEngine;
using VContainer.Unity;

namespace _Assets.Scripts.Services
{
    public class GameOverTimer : ITickable
    {
        public event Action OnTimerEnded;
        public event Action<float, float> OnTimerStarted;
        public event Action<float> OnTimerStopped;
        public event Action<float> OnTimeChanged;
        public float Time => _time;
        private bool _isRunning;
        private float _time;
        private const float StartTime = 0f;//5f;

        public void StartTimer()
        {
            if (_isRunning) return;
            _isRunning = true;
            _time = StartTime;
            Debug.LogError("Start Timer");
            OnTimerStarted?.Invoke(StartTime, _time);
        }

        public void StopTimer()
        {
            if (!_isRunning) return;
            _isRunning = false;
            _time = StartTime;
            Debug.LogError("Stop Timer");
            OnTimerStopped?.Invoke(_time);
        }

        public void Tick()
        {
            if (_isRunning)
            {
                _time = Mathf.Clamp(_time - UnityEngine.Time.deltaTime, 0, StartTime);

                OnTimeChanged?.Invoke(_time);

                if (_time == 0)
                {
                    _isRunning = false;
                    OnTimerEnded?.Invoke();
                    Debug.LogError("Timer ended");
                }
            }
        }
    }
}