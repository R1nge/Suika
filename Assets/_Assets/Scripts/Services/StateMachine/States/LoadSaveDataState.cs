using System.Collections;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class LoadSaveDataState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPlayerDataLoader _playerDataLoader;
        private readonly UIStateMachine _uiStateMachine;
        private readonly CoroutineRunner _coroutineRunner;
        private readonly IConfigLoader _configLoader;

        public LoadSaveDataState(GameStateMachine stateMachine, IPlayerDataLoader playerDataLoader, UIStateMachine uiStateMachine, CoroutineRunner coroutineRunner, IConfigLoader configLoader)
        {
            _stateMachine = stateMachine;
            _playerDataLoader = playerDataLoader;
            _uiStateMachine = uiStateMachine;
            _coroutineRunner = coroutineRunner;
            _configLoader = configLoader;
        }

        public void Enter() => _coroutineRunner.StartCoroutine(WaitForData());

        private IEnumerator WaitForData()
        {
            _uiStateMachine.SwitchState(UIStateType.Loading);
            _playerDataLoader.LoadData();
            _configLoader.LoadAllConfigs();
            yield return new WaitForSeconds(2f);
            _uiStateMachine.SwitchState(UIStateType.MainMenu);
        }

        public void Exit()
        {
        }
    }
}