﻿using _Assets.Scripts.Services.Configs;
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

        public void Enter() => _ui = _objectResolver.Instantiate(_configProvider.UIConfig.InGameMenu);

        public void Exit() => Object.Destroy(_ui);
    }
}