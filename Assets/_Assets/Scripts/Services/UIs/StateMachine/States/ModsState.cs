﻿using _Assets.Scripts.Services.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class ModsState : IUIState
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private GameObject _ui;

        public ModsState(ConfigProvider configProvider, IObjectResolver objectResolver)
        {
            _configProvider = configProvider;
            _objectResolver = objectResolver;
        }

        public void Enter() => _ui = _objectResolver.Instantiate(_configProvider.UIConfig.ModsMenu);

        public void Exit() => Object.Destroy(_ui);
    }
}