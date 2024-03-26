using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Assets.Scripts.Services.UIs
{
    public class MyButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        public event Action<bool> OnSelectStateChanged;


        private void LateUpdate()
        {
            if (Time.frameCount % 10 != 0)
                return;

            Debug.Log("Event sys" + EventSystem.current.currentSelectedGameObject.GetInstanceID());
            Debug.Log("button" + button.gameObject.GetInstanceID());

            if (EventSystem.current.currentSelectedGameObject.GetInstanceID() == button.gameObject.GetInstanceID())
            {
                Select();
            }
            else
            {
                Deselect();
            }
        }


        private bool _selected;

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnSelectStateChanged?.Invoke(_selected);
            }
        }

        private void Select()
        {
            if (Selected) return;
            Selected = true;
        }

        private void Deselect()
        {
            if (!Selected) return;
            Selected = false;
        }
    }
}