using System;
using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Factories;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class Suika : MonoBehaviour
    {
        //TODO: when suikas collide, destroy them and spawn one at the middle of them (between) with almost zero scale and scale it up to the original size
        public bool IsLanded => _landed;
        private int _index;
        private bool _collided;
        private bool _landed;
        [Inject] private SuikasFactory _suikasFactory;
        [Inject] private ScoreService _scoreService;


        public void SetIndex(int index) => _index = index;

        private void OnCollisionEnter2D(Collision2D other)
        {
            _landed = true;
            if (other.gameObject.TryGetComponent(out Suika suika))
            {
                if (suika._index == _index)
                {
                    if (_collided || suika._collided) return;
                    _collided = true;
                    suika._collided = true;
                    var middle = (transform.position + suika.transform.position) / 2f;
                    //Or move it to the another suika position
                    //var suikaPosition = suika.transform.position;
                    //newSuikaInstance.transform.position = suikaPosition;
                    _suikasFactory.Create(_index, middle);
                    Destroy(gameObject);
                    Destroy(suika.gameObject);
                }
            }
        }
    }
}