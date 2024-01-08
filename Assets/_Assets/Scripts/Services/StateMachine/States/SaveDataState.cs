using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.Datas.Mods;
using _Assets.Scripts.Services.Datas.Player;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class SaveDataState : IGameState
    {
        private readonly IPlayerDataLoader _playerDataLoader;
        private readonly IModDataLoader _modDataLoader;
        private readonly IAudioSettingsLoader _audioSettingsLoader;

        public SaveDataState(IPlayerDataLoader playerDataLoader, IModDataLoader modDataLoader, IAudioSettingsLoader audioSettingsLoader)
        {
            _playerDataLoader = playerDataLoader;
            _modDataLoader = modDataLoader;
            _audioSettingsLoader = audioSettingsLoader;
        }

        public void Enter()
        {
            _playerDataLoader.SaveData();
            _modDataLoader.Save();
            _audioSettingsLoader.Save();
        }

        public void Exit()
        {
        }
    }
}