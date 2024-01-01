using _Assets.Scripts.Services.Datas;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class SaveDataState : IGameState
    {
        private readonly IPlayerDataLoader _playerDataLoader;

        public SaveDataState(IPlayerDataLoader playerDataLoader)
        {
            _playerDataLoader = playerDataLoader;
        }

        public void Enter()
        {
            _playerDataLoader.SaveData();
        }

        public void Exit()
        {
        }
    }
}