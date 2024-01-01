using _Assets.Scripts.Services.Datas.GameConfigs;
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
        private int _index;

        private void Start() => select.onClick.AddListener(Select);

        public void SetSlotData(Sprite modIcon, string modName, int index)
        {
            this.modIcon.sprite = modIcon;
            modNameText.text = modName;
            _index = index;
        }

        private void Select()
        {
            _configLoader.SetCurrentConfig(_index);
            Debug.LogError($"SELECTED {_index}");
        }

        private void OnDestroy() => select.onClick.RemoveAllListeners();
    }
}