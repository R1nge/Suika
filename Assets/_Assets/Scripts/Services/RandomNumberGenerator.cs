﻿using System;
using _Assets.Scripts.Services.Datas.GameConfigs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Assets.Scripts.Services
{
    public class RandomNumberGenerator
    {
        private int _previous, _current, _previousNext, _next;
        private readonly IConfigLoader _configLoader;
        private bool _isFirstCall = true;

        private RandomNumberGenerator(IConfigLoader configLoader) => _configLoader = configLoader;

        public event Action<int, int, int> OnSuikaPicked;

        public int Previous => _previous;
        public int Current => _current;
        public int Next => _next;

        public int PickRandomSuika()
        {
            if (_isFirstCall)
            {
                _isFirstCall = false;
                _current = Random.Range(0, 5);
                _next = Random.Range(0, 5);
            }

            var chance = Random.Range(0f, 1f);
            var next = Random.Range(0, 5);

            if (chance <= _configLoader.CurrentConfig.SuikaDropChances[next])
            {
                _previous = _current;
                _current = _next;
                _next = next;

                OnSuikaPicked?.Invoke(_previous, _current, _next);
            }
            else
            {
                return PickRandomSuika();
            }


            return _current;
        }
    }
}