﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace _Assets.Scripts
{
    public class SetSelectedUIElement : MonoBehaviour
    {
        [SerializeField] private GameObject selected;

        private void Awake() => EventSystem.current.SetSelectedGameObject(selected.gameObject);
    }
}