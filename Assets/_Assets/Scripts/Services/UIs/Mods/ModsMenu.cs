using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
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

        private async void CreateSlot(int index)
        {
            var slot = _modSlotFactory.CreateSlot();
            slot.transform.SetParent(slotParent);
            slot.transform.localScale = Vector3.one;

            var iconPath = _configLoader.AllConfigs[index].ModIconPath;

            if (index == 0)
            {
                var iconSprite = await SpriteHelper.CreateSpriteFromStreamingAssests(iconPath, 128, 128);
                slot.SetSlotData(iconSprite, _configLoader.AllConfigs[index].ModName, index);
            }
            else
            {
                var iconSprite = await SpriteHelper.CreateSprite(iconPath, 128, 128);
                slot.SetSlotData(iconSprite, _configLoader.AllConfigs[index].ModName, index);
            }
        }
    }
}