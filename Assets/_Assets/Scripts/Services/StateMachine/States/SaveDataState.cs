using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Datas.Mods;
using _Assets.Scripts.Services.Datas.Player;
using _Assets.Scripts.Services.Vibrations;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class SaveDataState : IGameState
    {
        private readonly IPlayerDataLoader _playerDataLoader;
        private readonly IModDataLoader _modDataLoader;
        private readonly IAudioSettingsLoader _audioSettingsLoader;
        private readonly ContinueGameService _continueGameService;
        private readonly IVibrationSettingLoader _vibrationSettingLoader;

        public SaveDataState(IPlayerDataLoader playerDataLoader, IModDataLoader modDataLoader, IAudioSettingsLoader audioSettingsLoader, ContinueGameService continueGameService, IVibrationSettingLoader vibrationSettingLoader)
        {
            _playerDataLoader = playerDataLoader;
            _modDataLoader = modDataLoader;
            _audioSettingsLoader = audioSettingsLoader;
            _continueGameService = continueGameService;
            _vibrationSettingLoader = vibrationSettingLoader;
        }

        public async UniTask Enter()
        {
            _playerDataLoader.SaveData();
            _modDataLoader.Save();
            _audioSettingsLoader.Save();
            await _continueGameService.Save();
            _vibrationSettingLoader.Save();
        }

        public void Exit()
        {
        }
    }
}