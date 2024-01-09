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
        [Inject] private ContinueGameService _continueGameService;
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
            _continueGameService.Reset();
        }

        private void OnDestroy() => select.onClick.RemoveAllListeners();
    }
}