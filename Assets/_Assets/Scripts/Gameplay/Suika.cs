using System;
using _Assets.Scripts.Services.Factories;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class Suika : MonoBehaviour
    {
        //TODO: when suikas collide, destroy them and spawn one at the middle of them (between) with almost zero scale and scale it up to the original size
        private int _index;
        [Inject] private SuikasFactory _suikasFactory;

        public void SetIndex(int index)
        {
            _index = index;
        }

        // private void OnCollisionEnter2D(Collision2D other)
        // {
        //     if (other.gameObject.TryGetComponent(out Suika suika))
        //     {
        //         if (suika._index == _index)
        //         {
        //             _suikasFactory.CreateWithZeroScale(_index, transform.position);
        //         }
        //     }
        // }
    }
}