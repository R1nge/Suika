using System;
using UnityEngine;
using VContainer.Unity;

namespace _Assets.Scripts.Services
{
    public class TimeRushTimer : ITickable
    {
        private readonly float _maxTime = 60f * 5f;
        private float _currentTime;
        private bool _enabled;
        public float CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnTimerChanged?.Invoke(_currentTime);
            }
        }

        public event Action<float> OnTimerChanged;
        
        public void Start()
        {
            _currentTime = _maxTime;
            _enabled = true;
        }

        public void Tick()
        {
            if (_enabled)
            {
                CurrentTime -= Time.deltaTime;
            }    
        }

        public void Pause()
        {
            _enabled = false;
        }
        
        public void Resume()
        {
            _enabled = true;
        }

        public void Stop()
        {
            _enabled = false;
        }
    }
}