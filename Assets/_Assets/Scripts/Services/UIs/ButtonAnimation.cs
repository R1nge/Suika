using DG.Tweening;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs
{
    public class ButtonAnimation : MonoBehaviour
    {
        [SerializeField] private MyButton button;
        private Sequence _sequence;

        private void Start()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(button.transform.DOLocalRotate(new Vector3(0, 0, 10), .25f));
            _sequence.Append(button.transform.DOLocalRotate(new Vector3(0, 0, 0), .12f));
            _sequence.Append(button.transform.DOLocalRotate(new Vector3(0, 0, -10), .25f));
            _sequence.Append(button.transform.DOLocalRotate(new Vector3(0, 0, 0), .12f));
            _sequence.SetLoops(-1);
            _sequence.Pause();

            button.OnSelectStateChanged += StateChanged;
        }

        private void StateChanged(bool selected)
        {
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
            _sequence.Restart();
        }

        private void StopAnimation()
        {
            if (_sequence.active)
            {
                _sequence.TogglePause();
            }

            button.transform.DOLocalRotate(Vector3.zero, .12f);
        }

        private void OnDestroy() => _sequence.Kill();
    }
}