using _Assets.Scripts.Services.UIs.Mods;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class ModsState : IUIState
    {
        private readonly UIFactory _uiFactory;
        private ModsMenu _ui;

        public ModsState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public async UniTask Enter()
        {
            _ui = _uiFactory.CreateModUI();
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