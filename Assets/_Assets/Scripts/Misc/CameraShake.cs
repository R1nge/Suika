using System;
using _Assets.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace _Assets.Scripts.Misc
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [Inject] private GameOverTimer _gameOverTimer;
        private float _shakeMagnitude;
        private Vector3 _originalPos;
        private bool _shaking;

        private void Start()
        {
            _gameOverTimer.OnTimerStarted += StartShake;
            _gameOverTimer.OnTimerStopped += StopShake;
            _gameOverTimer.OnTimerEnded += StopShake;
        }

        private void StopShake(float _) => StopShake();

        private void StopShake()
        {
            _shaking = false;
            camera.transform.localPosition = _originalPos;
        }

        private void StartShake(float _, float __)
        {
            if (!_shaking)
            {
                _originalPos = camera.transform.localPosition;
                _shakeMagnitude = 0.5f;
                _shaking = true;
            }
        }

        private async void Update()
        {
            if (_shaking)
            {
                var x = Random.Range(-1f, 1f) * _shakeMagnitude;
                camera.transform.localPosition = new Vector3(_originalPos.x + x, _originalPos.y, _originalPos.z);
                await UniTask.DelayFrame(10);
            }
        }

        private void OnDestroy()
        {
            _gameOverTimer.OnTimerStarted -= StartShake;
            _gameOverTimer.OnTimerStopped -= StopShake;
            _gameOverTimer.OnTimerEnded -= StopShake;
        }
    }
}