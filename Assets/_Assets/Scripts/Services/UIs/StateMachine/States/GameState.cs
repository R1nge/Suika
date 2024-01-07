using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.UIs.InGame;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class GameState : IUIState
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private GameObject _ui;

        public GameState(ConfigProvider configProvider, IObjectResolver objectResolver)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
        }

        public void Enter()
        {
            _ui = _objectResolver.Instantiate(_configProvider.UIConfig.InGameMenu);
            _ui.GetComponent<NextSuikaUI>().Init();
            _ui.GetComponent<InGameTimer>().Init();
            _ui.GetComponent<ScoreUI>().Init();
            _ui.GetComponent<InGameBackground>().Init().Forget();
        }

        public async void Exit(int millisecondsDelay)
        {
            await UniTask.Delay(millisecondsDelay);
            Object.Destroy(_ui);
        }
    }
}