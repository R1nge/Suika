using _Assets.Scripts.Services.Datas;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class GameOverState : IGameState
    {
        private readonly IDataService _dataService;
        
        public GameOverState(IDataService dataService)
        {
            _dataService = dataService;
        }
        
        public void Enter()
        {
            //TODO: Show Game Over (Retry, main menu)
            _dataService.SaveData();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}