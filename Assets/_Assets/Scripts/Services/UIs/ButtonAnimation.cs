using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Assets.Scripts.Services.UIs
{
    public class ButtonAnimation : MonoBehaviour
    {
        [SerializeField] private MyButton button;

        private Sequence _sequence;

        private void Start()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(button.transform.DOLocalRotate(new Vector3(0, 0, 20), .12f));
            _sequence.Append(button.transform.DOLocalRotate(new Vector3(0, 0, 0), .12f));
            _sequence.Append(button.transform.DOLocalRotate(new Vector3(0, 0, -20), .12f));
            _sequence.Append(button.transform.DOLocalRotate(new Vector3(0, 0, 0), .12f));
            _sequence.SetLoops(-1);

            button.OnSelectStateChanged += StateChanged;
        }

        private void StateChanged(bool selected)
        {
            Debug.Log(gameObject.name + "State changed " + selected);
            if (selected)
            {
                StartAnimation();
            }
            else
            {
                StopAnimation();
            }
        }

        private void StartAnimation()
        {
            _sequence.Play();
        }

        private void StopAnimation()
        {
            if (_sequence.active)
            {
                _sequence.TogglePause();       
            }
          
            button.transform.DOLocalRotate(Vector3.zero, .12f);
        }
    }
}