using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class LoadSaveDataState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPlayerDataLoader _playerDataLoader;
        private readonly UIStateMachine _uiStateMachine;
        private readonly IConfigLoader _configLoader;

        public LoadSaveDataState(GameStateMachine stateMachine, IPlayerDataLoader playerDataLoader, UIStateMachine uiStateMachine, IConfigLoader configLoader)
        {
            _stateMachine = stateMachine;
            _playerDataLoader = playerDataLoader;
            _uiStateMachine = uiStateMachine;
            _configLoader = configLoader;
        }

        public async void Enter()
        {
            _uiStateMachine.SwitchState(UIStateType.Loading);
            _playerDataLoader.LoadData();
            _configLoader.LoadDefaultConfig();
            _configLoader.LoadAllConfigs();
            await UniTask.Delay(500);
            _uiStateMachine.SwitchState(UIStateType.MainMenu);
        }

        public void Exit()
        {
        }
    }
}