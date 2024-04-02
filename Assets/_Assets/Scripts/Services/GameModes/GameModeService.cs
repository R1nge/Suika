using System.Linq;
using _Assets.Scripts.Services.Configs;

namespace _Assets.Scripts.Services.GameModes
{
    public class GameModeService
    {
        private readonly ConfigProvider _configProvider;
        public GameMode SelectedGameMode { get; set; }
        private GameModeConfig _gameModeConfig;
        public GameModeConfig GameModeConfig => _gameModeConfig;

        private GameModeService(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void SelectGameMode(GameMode gameMode)
        {
            SelectedGameMode = gameMode;
            _gameModeConfig = _configProvider.GameModeConfigs.First(config => config.GameMode == SelectedGameMode);
        }

        public enum GameMode
        {
            Classic,
            TimeRush
        }
    }
}