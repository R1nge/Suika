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

        public UIStateMachine(UIStatesFactory uiStatesFactory)
        {
            _states = new Dictionary<UIStateType, IUIState>
            {
                { UIStateType.Loading, uiStatesFactory.CreateLoadingState(this) },
                { UIStateType.MainMenu, uiStatesFactory.CreateMainMenuState(this) },
                { UIStateType.Mods, uiStatesFactory.CreateModsState(this) },
                { UIStateType.Game, uiStatesFactory.CreateGameState(this) },
                { UIStateType.GameOver, uiStatesFactory.CreateGameOverState(this) }
            };
        }

        public async UniTask SwitchState(UIStateType uiStateType, int switchDelayInMilliseconds = 0, int previousStateExitDelayInMilliseconds = 0)
        {
            if (_currentUIStateType == uiStateType)
            {
                Debug.LogError($"Already in {_currentUIStateType} state");
                return;
            }

            await UniTask.Delay(switchDelayInMilliseconds);

            _previousUIState = _currentUIState;
            
            _currentUIState = _states[uiStateType];
            _currentUIStateType = uiStateType;
            _currentUIState.Enter();
            
            
            if (previousStateExitDelayInMilliseconds != 0)
            {
                await UniTask.Delay(previousStateExitDelayInMilliseconds);
                _previousUIState?.Exit();
            }
            else
            {
                _previousUIState?.Exit();
            }
            
            //_currentUIState?.Exit();
            
        }
    }
}