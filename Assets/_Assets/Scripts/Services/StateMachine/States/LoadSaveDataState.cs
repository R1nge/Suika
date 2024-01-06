using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.Datas.Mods;
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
        private readonly IModDataLoader _modDataLoader;

        public LoadSaveDataState(GameStateMachine stateMachine, IPlayerDataLoader playerDataLoader, UIStateMachine uiStateMachine, IConfigLoader configLoader, IModDataLoader modDataLoader)
        {
            _stateMachine = stateMachine;
            _playerDataLoader = playerDataLoader;
            _uiStateMachine = uiStateMachine;
            _configLoader = configLoader;
            _modDataLoader = modDataLoader;
        }

        public async void Enter()
        {
            await _modDataLoader.Load();
            _playerDataLoader.LoadData();
            await _configLoader.LoadDefaultConfig();
            _configLoader.LoadAllConfigs();
            _configLoader.SetCurrentConfig(_modDataLoader.ModData.SelectedModIndex);
            _uiStateMachine.SwitchState(UIStateType.Loading);
            await UniTask.Delay(500);
            _uiStateMachine.SwitchState(UIStateType.MainMenu);
        }

        public void Exit()
        {
        }
    }
}