using _Assets.Scripts.Services.Datas.Mods;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class MainMenuState : IUIState
    {
        private readonly UIFactory _uiFactory;
        private readonly ContinueGameService _continueGameService;
        private readonly IModDataLoader _modDataLoader;
        private GameObject _ui;
        
        public MainMenuState(UIFactory uiFactory, ContinueGameService continueGameService, IModDataLoader modDataLoader)
        {
            _uiFactory = uiFactory;
            _continueGameService = continueGameService;
            _modDataLoader = modDataLoader;
        }

        public async UniTask Enter()
        {
            _continueGameService.DeleteContinueData();
            _modDataLoader.Save();
            var ui = await _uiFactory.CreateMainMenuUI();
            _ui = ui.gameObject;
        }

        public void Exit() => Object.Destroy(_ui);
    }
}