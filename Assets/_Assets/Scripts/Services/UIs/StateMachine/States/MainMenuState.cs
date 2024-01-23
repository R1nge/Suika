using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class MainMenuState : IUIState
    {
        private readonly UIFactory _uiFactory;
        private GameObject _ui;
        
        public MainMenuState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public async UniTask Enter()
        {
            var ui = await _uiFactory.CreateMainMenuUI();
            _ui = ui.gameObject;
        }

        public void Exit() => Object.Destroy(_ui);
    }
}