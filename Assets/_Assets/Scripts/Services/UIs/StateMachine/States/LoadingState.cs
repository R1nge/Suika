using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class LoadingState : IUIState
    {
        private readonly UIFactory _uiFactory;
        private LoadingCurtain _ui;
        
        public LoadingState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public async UniTask Enter()
        {
            _ui = await _uiFactory.CreateLoadingCurtain();
            await _ui.GetComponent<UICanvasAnimation>().Play(AnimationType.FadeIn);
        }

        public async UniTask Exit()
        {
            await _ui.GetComponent<UICanvasAnimation>().Play(AnimationType.FadeOut);
            await UniTask.DelayFrame(1);
            _ui.Hide();
        }
    }
}