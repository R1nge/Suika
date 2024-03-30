using _Assets.Scripts.Services.Datas.Mods;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class MainMenuState : IUIState
    {
        private readonly UIFactory _uiFactory;
        private readonly IModDataLoader _modDataLoader;
        private MainMenu _ui;
        
        public MainMenuState(UIFactory uiFactory, IModDataLoader modDataLoader)
        {
            _uiFactory = uiFactory;
            _modDataLoader = modDataLoader;
        }

        public async UniTask Enter()
        {
            _modDataLoader.Save();
            _ui = await _uiFactory.CreateMainMenuUI();
            await _ui.GetComponent<UICanvasAnimation>().Play(AnimationType.FadeIn);
        }

        public async UniTask Exit()
        {
            await _ui.GetComponent<UICanvasAnimation>().Play(AnimationType.FadeOut);
            await UniTask.DelayFrame(1);
            Object.Destroy(_ui.gameObject);
        }
    }
}