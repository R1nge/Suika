using System.Linq;
using _Assets.Scripts.Services.Configs;

namespace _Assets.Scripts.Services.GameModes
{
    public class GameModeService
    {
        private readonly ConfigProvider _configProvider;
        private GameMode _selectedGameMode;
        public GameMode SelectedGameMode => _selectedGameMode;
        private GameModeConfig _gameModeConfig;
        public GameModeConfig GameModeConfig => _gameModeConfig;

        private GameModeService(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void SelectGameMode(GameMode gameMode)
        {
            _selectedGameMode = gameMode;
            _gameModeConfig = _configProvider.GameModeConfigs.First(config => config.GameMode == _selectedGameMode);
        }

        public enum GameMode
        {
            Classic,
            TimeRush
        }
    }
}