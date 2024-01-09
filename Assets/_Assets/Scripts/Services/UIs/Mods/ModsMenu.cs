using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Datas.Mods;
using Cysharp.Threading.Tasks;
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
        [Inject] private IModDataLoader _modDataLoader;

        private void Start()
        {
            close.onClick.AddListener(SwitchToMainMenu);

            for (int i = 0; i < _configLoader.AllConfigs.Count; i++)
            {
                CreateSlot(i);
            }
        }

        private void SwitchToMainMenu()
        {
            _uiStateMachine.SwitchState(UIStateType.MainMenu).Forget();
            _modDataLoader.Save();
        }

        private async void CreateSlot(int index)
        {
            var iconPath = _configLoader.AllConfigs[index].ModIconPath;

            Sprite iconSprite;
            
            if (index == 0)
            {
                iconSprite = await SpriteHelper.CreateSpriteFromStreamingAssests(iconPath);
            }
            else
            {
                iconSprite = await SpriteHelper.CreateSprite(iconPath);
            }
            
            var slot = _modSlotFactory.CreateSlot();
            slot.transform.SetParent(slotParent);
            slot.transform.localScale = Vector3.one;
            slot.SetSlotData(iconSprite, _configLoader.AllConfigs[index].ModName);
        }
    }
}