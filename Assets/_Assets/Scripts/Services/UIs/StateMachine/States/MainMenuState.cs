﻿using _Assets.Scripts.Services.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class MainMenuState : IUIState
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private GameObject _ui;
        
        public MainMenuState(ConfigProvider configProvider, IObjectResolver objectResolver)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
        }

        public void Enter() => _ui = _objectResolver.Instantiate(_configProvider.UIConfig.MainMenu);

        public async void Exit(int millisecondsDelay)
        {
            await UniTask.Delay(millisecondsDelay);
            Object.Destroy(_ui);
        }
    }
}