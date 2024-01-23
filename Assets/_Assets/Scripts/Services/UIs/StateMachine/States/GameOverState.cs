using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class GameOverState : IUIState
    {
        private readonly UIFactory _uiFactory;
        private GameObject _ui;

        public GameOverState(UIFactory uiFactory) => _uiFactory = uiFactory;

        public UniTask Enter()
        {
            _ui = _uiFactory.CreateGameOverUI().gameObject;
            return UniTask.CompletedTask;
        }

        public void Exit() => Object.Destroy(_ui);
    }
}