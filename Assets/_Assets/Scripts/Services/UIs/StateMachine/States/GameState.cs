using _Assets.Scripts.Services.UIs.InGame;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class GameState : IUIState
    {
        private readonly UIFactory _uiFactory;
        private InGameBackground _ui;

        public GameState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public async UniTask Enter()
        {
            if (_ui == null)
            {
                _ui = await _uiFactory.CreateInGameUI();
                await _ui.GetComponent<UICanvasAnimation>().Play(AnimationType.FadeIn);
            }
        }

        public async UniTask Exit()
        {
            await _ui.GetComponent<UICanvasAnimation>().Play(AnimationType.FadeOut);
            await UniTask.DelayFrame(1);
            Object.Destroy(_ui.gameObject);
        }
    }
}