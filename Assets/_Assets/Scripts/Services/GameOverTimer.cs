using System;
using _Assets.Scripts.Services.Datas.GameConfigs;
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
        private readonly IConfigLoader _configLoader;

        public GameOverTimer(IConfigLoader configLoader) => _configLoader = configLoader;

        public void StartTimer()
        {
            if (_isRunning) return;
            _isRunning = true;
            _time = _configLoader.CurrentConfig.TimerStartTime;
            Debug.LogError("Start Timer");
            OnTimerStarted?.Invoke(_configLoader.CurrentConfig.TimerStartTime, _time);
        }

        public void StopTimer()
        {
            if (!_isRunning) return;
            _isRunning = false;
            _time = _configLoader.CurrentConfig.TimerStartTime;
            Debug.LogError("Stop Timer");
            OnTimerStopped?.Invoke(_time);
        }

        public void Tick()
        {
            if (_isRunning)
            {
                _time = Mathf.Clamp(_time - UnityEngine.Time.deltaTime, 0, _configLoader.CurrentConfig.TimerStartTime);

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