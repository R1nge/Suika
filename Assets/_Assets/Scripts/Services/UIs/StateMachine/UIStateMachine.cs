using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine
{
    public class UIStateMachine
    {
        private readonly Dictionary<UIStateType, IUIState> _states;
        private IUIState _currentUIState;
        private IUIState _previousUIState;
        private UIStateType _currentUIStateType;
        private UIStateType _previousUIStateType;

        public UIStateMachine(UIStatesFactory uiStatesFactory)
        {
            _states = new Dictionary<UIStateType, IUIState>
            {
                { UIStateType.Loading, uiStatesFactory.CreateLoadingState(this) },
                { UIStateType.MainMenu, uiStatesFactory.CreateMainMenuState(this) },
                { UIStateType.Mods, uiStatesFactory.CreateModsState(this) },
                { UIStateType.Game, uiStatesFactory.CreateGameState(this) },
                { UIStateType.GameOver, uiStatesFactory.CreateGameOverState(this) },
                { UIStateType.Settings, uiStatesFactory.CreateSettingsState(this) },
                { UIStateType.Pause, uiStatesFactory.CreateGamePauseState(this) }
            };
        }

        public async UniTask SwitchState(UIStateType uiStateType, int switchDelayInMilliseconds = 0)
        {
            if (_currentUIStateType == uiStateType)
            {
                Debug.LogError($"Already in {_currentUIStateType} state");
                return;
            }

            await UniTask.Delay(switchDelayInMilliseconds);

            _previousUIStateType = _currentUIStateType;
            _previousUIState = _currentUIState;
            
            _currentUIState = _states[uiStateType];
            _currentUIStateType = uiStateType;
            await _currentUIState.Enter();
            _previousUIState?.Exit();
        }

        public async UniTask SwitchToPreviousState(int switchDelayInMilliseconds = 0)
        {
            await SwitchState(_previousUIStateType, switchDelayInMilliseconds);
        }

        public async UniTask SwitchStateWithoutExitFromPrevious(UIStateType uiStateType, int switchDelayInMilliseconds = 0)
        {
            if (_currentUIStateType == uiStateType)
            {
                Debug.LogError($"Already in {_currentUIStateType} state");
                return;
            }
            
            await UniTask.Delay(switchDelayInMilliseconds);

            _currentUIState = _states[uiStateType];
            _currentUIStateType = uiStateType;
            await _currentUIState.Enter();
        }
    }
}