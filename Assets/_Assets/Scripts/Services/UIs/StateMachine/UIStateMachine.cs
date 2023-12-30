﻿using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStateMachine
    {
        private readonly Dictionary<UIStateType, IUIState> _states;
        private IUIState _currentUIState;
        private UIStateType _currentUIStateType;

        public UIStateMachine(UIStatesFactory uiStatesFactory)
        {
            _states = new Dictionary<UIStateType, IUIState>
            {
                { UIStateType.Loading, uiStatesFactory.CreateLoadingState(this) },
                { UIStateType.MainMenu, uiStatesFactory.CreateMainMenuState(this) },
                { UIStateType.Game, uiStatesFactory.CreateGameState(this) },
                { UIStateType.GameOver, uiStatesFactory.CreateGameOverState(this) }
            };
        }

        public void SwitchState(UIStateType uiStateType)
        {
            if (_currentUIStateType == uiStateType)
            {
                Debug.LogError($"Already in {_currentUIStateType} state");
                return;
            }

            _currentUIState?.Exit();
            _currentUIState = _states[uiStateType];
            _currentUIStateType = uiStateType;
            _currentUIState.Enter();
        }
    }
}