﻿using System;
using _Assets.Scripts.Services.Factories;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class Suika : MonoBehaviour
    {
        //TODO: when suikas collide, destroy them and spawn one at the middle of them (between) with almost zero scale and scale it up to the original size
        private int _index;
        private bool _collided;
        [Inject] private SuikasFactory _suikasFactory;

        public void SetIndex(int index)
        {
            _index = index;
        }

        private void Update()
        {
            if (transform.localScale.x < _index * .5f)
            {
                transform.localScale += Vector3.one * Time.deltaTime;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Suika suika))
            {
                if (suika._index == _index)
                {
                    if (_collided || suika._collided) return;
                    _collided = true;
                    suika._collided = true;
                    var middle = (transform.position + suika.transform.position) / 2f;
                    _suikasFactory.CreateWithZeroScale(_index, middle);
                    Destroy(gameObject);
                    Destroy(suika.gameObject);
                }
            }
        }
    }
}