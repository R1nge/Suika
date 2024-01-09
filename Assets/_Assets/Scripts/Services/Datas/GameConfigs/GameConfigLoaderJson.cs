using System.Collections.Generic;
using System.IO;
using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _Assets.Scripts.Services.Datas.GameConfigs
{
    public class GameConfigLoaderJson : IConfigLoader
    {
        private readonly GameConfigValidator _gameConfigValidator;
        private readonly SpritesCacheService _spritesCacheService;
        private GameConfig _currentConfig;
        private readonly List<GameConfig> _allConfigs = new();

        public List<GameConfig> AllConfigs => _allConfigs;
        public GameConfig CurrentConfig => _currentConfig;
        public bool IsDefault => _currentConfig.Equals(_allConfigs[0]);

        public GameConfigLoaderJson(GameConfigValidator gameConfigValidator, SpritesCacheService spritesCacheService)
        {
            _gameConfigValidator = gameConfigValidator;
            _spritesCacheService = spritesCacheService;
        }

        public async UniTask LoadDefaultConfig()
        {
            var targetFile = Path.Combine(PathsHelper.StreamingAssetsPath, "config.json");

            UnityWebRequest www = UnityWebRequest.Get(targetFile);
            await www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var json = www.downloadHandler.text;
                var config = JsonConvert.DeserializeObject<GameConfig>(json);

                var modIconRelativePath = Path.Combine(PathsHelper.StreamingAssetsPath, config.ModIconPath);
                config.ModIconPath = modIconRelativePath;

                var containerImageRelativePath = Path.Combine(PathsHelper.StreamingAssetsPath, config.ContainerImagePath);
                config.ContainerImagePath = containerImageRelativePath;

                for (int i = 0; i < config.SuikaSkinsImagesPaths.Length; i++)
                {
                    var skinImageRelativePath = Path.Combine(PathsHelper.StreamingAssetsPath, config.SuikaSkinsImagesPaths[i]);
                    config.SuikaSkinsImagesPaths[i] = skinImageRelativePath;
                }

                for (int i = 0; i < config.SuikaIconsPaths.Length; i++)
                {
                    var suikaIconsRelativePath = Path.Combine(PathsHelper.StreamingAssetsPath, config.SuikaIconsPaths[i]);
                    config.SuikaIconsPaths[i] = suikaIconsRelativePath;
                }

                for (int i = 0; i < config.SuikaAudios.Length; i++)
                {
                    var suikaAudioRelativePath = Path.Combine(PathsHelper.StreamingAssetsPath, config.SuikaAudios[i].Path);
                    config.SuikaAudios[i].Path = suikaAudioRelativePath;
                }

                if (config.TimeBeforeTimerTrigger < 0)
                {
                    Debug.LogError(
                        $"TimeBeforeTimerTrigger is less than 0. Setting from default config. Value: {_allConfigs[0].TimeBeforeTimerTrigger}");
                }

                if (config.TimerStartTime < 0)
                {
                    Debug.LogError(
                        $"TimerStartTime is less than 0. Setting from default config. Value: {_allConfigs[0].TimerStartTime}");
                }

                var inGameBackground = Path.Combine(PathsHelper.StreamingAssetsPath, config.InGameBackgroundPath);
                config.InGameBackgroundPath = inGameBackground;

                var loadingScreenBackground = Path.Combine(PathsHelper.StreamingAssetsPath, config.LoadingScreenBackgroundPath);
                config.LoadingScreenBackgroundPath = loadingScreenBackground;

                var loadingScreenIcon = Path.Combine(PathsHelper.StreamingAssetsPath, config.LoadingScreenIconPath);
                config.LoadingScreenIconPath = loadingScreenIcon;

                var playerSkin = Path.Combine(PathsHelper.StreamingAssetsPath, config.PlayerSkinPath);
                config.PlayerSkinPath = playerSkin;

                for (int i = 0; i < config.MergeSoundsAudios.Length; i++)
                {
                    var mergeSoundsAudioRelativePath = Path.Combine(PathsHelper.StreamingAssetsPath, config.MergeSoundsAudios[i].Path);
                    config.MergeSoundsAudios[i].Path = mergeSoundsAudioRelativePath;
                }

                var mainMenuBackground = Path.Combine(PathsHelper.StreamingAssetsPath, config.MainMenuBackgroundPath);
                config.MainMenuBackgroundPath = mainMenuBackground;

                _allConfigs.Add(config);

                await UniTask.Delay(100);
            }
        }

        public void LoadAllConfigs()
        {
            var modsFoldersDirectoryInfo = new DirectoryInfo(PathsHelper.ModsPath);

            if (!modsFoldersDirectoryInfo.Exists)
            {
                modsFoldersDirectoryInfo.Create();
                return;
            }

            foreach (var directoryInfo in modsFoldersDirectoryInfo.GetDirectories())
            {
                foreach (var fileInfo in directoryInfo.GetFiles("*.json"))
                {
                    StreamReader reader = new StreamReader(fileInfo.FullName);
                    var json = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                    var config = JsonConvert.DeserializeObject<GameConfig>(json);

                    var defaultConfig = _allConfigs[0];
                    _gameConfigValidator.Validate(ref defaultConfig, ref config);

                    _allConfigs.Add(config);
                }
            }
        }


        public void SetCurrentConfig(string modName)
        {
            _spritesCacheService.Reset();

            for (int i = 0; i < _allConfigs.Count; i++)
            {
                if (_allConfigs[i].ModName == modName)
                {
                    _currentConfig = _allConfigs[i];
                    _spritesCacheService.Preload(_currentConfig, IsDefault).Forget();
                    break;
                }

                Debug.LogError($"Mod not found: {modName}");
            }
        }
    }
}