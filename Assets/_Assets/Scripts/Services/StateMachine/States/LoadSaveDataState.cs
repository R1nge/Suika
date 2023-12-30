using _Assets.Scripts.Services.Datas;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class LoadSaveDataState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IDataService _dataService;

        public LoadSaveDataState(GameStateMachine stateMachine, IDataService dataService)
        {
            _stateMachine = stateMachine;
            _dataService = dataService;
        }

        public void Enter()
        {
            _dataService.LoadData();
        }
        
        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}