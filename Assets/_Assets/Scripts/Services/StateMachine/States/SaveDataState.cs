using _Assets.Scripts.Services.Datas;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class SaveDataState : IGameState
    {
        private readonly IDataService _dataService;

        public SaveDataState(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Enter()
        {
            _dataService.SaveData();
        }

        public void Exit()
        {
        }
    }
}