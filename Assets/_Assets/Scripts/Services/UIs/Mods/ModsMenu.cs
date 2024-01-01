using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using VContainer;

namespace _Assets.Scripts.Services.UIs.Mods
{
    public class ModsMenu : MonoBehaviour
    {
        [SerializeField] private Transform slotParent;
        [SerializeField] private Button close;
        [Inject] private ModSlotFactory _modSlotFactory;
        [Inject] private IConfigLoader _configLoader;
        [Inject] private UIStateMachine _uiStateMachine;

        private void Start()
        {
            close.onClick.AddListener(SwitchToMainMenu);
            
            for (int i = 0; i < _configLoader.AllConfigs.Count; i++)
            {
                CreateSlot(i);
            }
        }

        private void SwitchToMainMenu() => _uiStateMachine.SwitchState(UIStateType.MainMenu);

        private void CreateSlot(int index)
        {
            var slot = _modSlotFactory.CreateSlot();
            slot.transform.SetParent(slotParent);
            slot.transform.localScale = Vector3.one;
            var iconPath = _configLoader.AllConfigs[index].ModIconPath;
            var iconBytes = File.ReadAllBytes(iconPath);
            var iconTexture = new Texture2D(128, 128);
            iconTexture.LoadImage(iconBytes);
            var iconSprite = Sprite.Create(iconTexture, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));
            slot.SetSlotData(iconSprite, _configLoader.AllConfigs[index].ModName, index);
        }
    }
}