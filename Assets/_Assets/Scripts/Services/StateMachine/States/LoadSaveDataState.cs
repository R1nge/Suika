using System.Collections;
using System.Threading.Tasks;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class LoadSaveDataState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IDataService _dataService;
        private readonly UIStateMachine _uiStateMachine;
        private readonly CoroutineRunner _coroutineRunner;

        public LoadSaveDataState(GameStateMachine stateMachine, IDataService dataService, UIStateMachine uiStateMachine,
            CoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _dataService = dataService;
            _uiStateMachine = uiStateMachine;
            _coroutineRunner = coroutineRunner;
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(WaitForData());
        }

        private IEnumerator WaitForData()
        {
            _uiStateMachine.SwitchState(UIStateType.Loading);
            _dataService.LoadData();
            yield return new WaitForSeconds(2f);
            //TODO: wait for a couple of seconds
            _uiStateMachine.SwitchState(UIStateType.MainMenu);
        }

        public void Exit()
        {
        }
    }
}