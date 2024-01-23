using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.Mods
{
    public class ModsMenu : MonoBehaviour
    {
        [SerializeField] private Transform slotParent;
        [SerializeField] private Button close;
        [SerializeField] private Image background;
        [Inject] private ModSlotFactory _modSlotFactory;
        [Inject] private IConfigLoader _configLoader;
        [Inject] private UIStateMachine _uiStateMachine;
        [Inject] private SpriteCreator _spriteCreator;

        public void Init(Sprite sprite) => background.sprite = sprite;

        private async void Start()
        {
            //Not the best solution, but it works.
            _configLoader.ConfigChanged += ChangeBackground;
            close.onClick.AddListener(SwitchToMainMenu);
            
            var sprite = await _spriteCreator.CreateMainMenuBackground();
            Init(sprite);

            for (int i = 0; i < _configLoader.AllConfigs.Count; i++)
            {
                await CreateSlot(i);
            }
        }

        private async void ChangeBackground(GameConfig config)
        {
            var sprite = await _spriteCreator.CreateMainMenuBackground();
            Init(sprite);
        }

        private void SwitchToMainMenu() => _uiStateMachine.SwitchState(UIStateType.MainMenu).Forget();

        private async UniTask CreateSlot(int index)
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

        private void OnDestroy() => _configLoader.ConfigChanged -= ChangeBackground;
    }
}