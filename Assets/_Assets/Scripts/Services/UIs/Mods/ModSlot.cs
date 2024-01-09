﻿using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.Datas.Mods;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.Mods
{
    public class ModSlot : MonoBehaviour
    {
        [SerializeField] private Image modIcon;
        [SerializeField] private TextMeshProUGUI modNameText;
        [SerializeField] private Button select;
        [Inject] private IConfigLoader _configLoader;
        [Inject] private ContinueGameService _continueGameService;
        [Inject] private IModDataLoader _modDataLoader;
        private string _modName;

        private void Start() => select.onClick.AddListener(Select);

        public void SetSlotData(Sprite modIcon, string modName)
        {
            this.modIcon.sprite = modIcon;
            _modName = modName;
            modNameText.text = _modName;
        }

        private void Select()
        {
            _configLoader.SetCurrentConfig(_modName);
            _modDataLoader.SetModName(_modName);
            _continueGameService.Reset();
        }

        private void OnDestroy() => select.onClick.RemoveAllListeners();
    }
}