using System;
using UnityEngine;
using Random = System.Random;

namespace _Assets.Scripts.Services
{
    public class RandomNumberGenerator
    {
        private readonly Random _random = new();
        private int _previous, _current, _next;

        public event Action<int, int, int> OnSuikaPicked;

        public int Previous => _previous;
        public int Current => _current;
        public int Next => _next;

        public int PickRandomSuika()
        {
            _previous = _current;
            _current = _next;
            _next = _random.Next(0, 5);
            
            Debug.Log($"Previous: {_previous}, Current: {_current}, Next: {_next}");
            
            OnSuikaPicked?.Invoke(_previous, _current, _next);
            
            return _current;
        }
    }
}