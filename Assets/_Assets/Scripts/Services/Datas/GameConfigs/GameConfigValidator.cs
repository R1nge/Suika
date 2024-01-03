using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace _Assets.Scripts.Services.Datas.GameConfigs
{
    public class GameConfigValidator
    {
        private readonly string _modsPath = Path.Combine(Application.persistentDataPath, "Mods");

        public void Validate(ref GameConfig defaultConfig, ref GameConfig config)
        {
            ValidateModIconPath(ref defaultConfig, ref config);
            ValidateContainerImagePath(ref defaultConfig, ref config);
            ValidateSuikaSkinsImages(ref defaultConfig, ref config);
            ValidateSuikaIcons(ref defaultConfig, ref config);
            ValidateSuikaAudio(ref defaultConfig, ref config);
            ValidateSuikaDropChances(ref defaultConfig, ref config);
            ValidateTimeBeforeTimerTrigger(ref defaultConfig, ref config);
            ValidateTimerStartTime(ref defaultConfig, ref config);
            //Save(ref config);
        }

        private void ValidateModIconPath(ref GameConfig defaultConfig, ref GameConfig config)
        {
            config.ModIconPath = GetFilePath(config.ModIconPath, defaultConfig.ModIconPath, config.ModName);
        }

        private void ValidateContainerImagePath(ref GameConfig defaultConfig, ref GameConfig config)
        {
            config.ContainerImagePath = GetFilePath(config.ContainerImagePath, defaultConfig.ContainerImagePath, config.ModName);
        }

        private void ValidateSuikaSkinsImages(ref GameConfig defaultConfig, ref GameConfig config)
        {
            for (int i = 0; i < config.SuikaSkinsImagesPaths.Length; i++)
            {
                config.SuikaSkinsImagesPaths[i] = GetFilePath(config.SuikaSkinsImagesPaths[i],
                    defaultConfig.SuikaSkinsImagesPaths[i], config.ModName);
            }
        }

        private void ValidateSuikaIcons(ref GameConfig defaultConfig, ref GameConfig config)
        {
            for (int i = 0; i < config.SuikaIconsPaths.Length; i++)
            {
                config.SuikaIconsPaths[i] = GetFilePath(config.SuikaIconsPaths[i], defaultConfig.SuikaIconsPaths[i],
                    config.ModName);
            }
        }

        private void ValidateSuikaAudio(ref GameConfig defaultConfig, ref GameConfig config)
        {
            for (int i = 0; i < config.SuikaAudioPaths.Length; i++)
            {
                config.SuikaAudioPaths[i] = GetFilePath(config.SuikaAudioPaths[i], defaultConfig.SuikaAudioPaths[i],
                    config.ModName);
            }
        }

        private void ValidateSuikaDropChances(ref GameConfig defaultConfig, ref GameConfig config)
        {
            if (config.SuikaDropChances.Length < defaultConfig.SuikaDropChances.Length)
            {
                var originalLength = config.SuikaDropChances.Length;
                Array.Resize(ref config.SuikaDropChances, defaultConfig.SuikaDropChances.Length);

                for (int i = originalLength; i < config.SuikaDropChances.Length; i++)
                {
                    config.SuikaDropChances[i] = defaultConfig.SuikaDropChances[i];
                    Debug.LogError(
                        $"Mod: {config.ModName} Missing drop chance at index {i}. Setting from default config. Value: {defaultConfig.SuikaDropChances[i]}");
                }
            }
            else if (config.SuikaDropChances.Length != defaultConfig.SuikaDropChances.Length)
            {
                Debug.LogError($"Mod: {config.ModName} have {config.SuikaDropChances.Length} drop chances. But should have {defaultConfig.SuikaDropChances.Length}. Trimming");
                config.SuikaDropChances = config.SuikaDropChances.Take(defaultConfig.SuikaDropChances.Length).ToArray();
            }
        }

        private void ValidateTimeBeforeTimerTrigger(ref GameConfig defaultConfig, ref GameConfig config)
        {
            if (config.TimeBeforeTimerTrigger < 0)
            {
                Debug.LogError($"Mod: {config.ModName} TimeBeforeTimerTrigger is less than 0. Setting from default config. Value: {defaultConfig.TimeBeforeTimerTrigger}");
                config.TimeBeforeTimerTrigger = defaultConfig.TimeBeforeTimerTrigger;
            }
        }

        private void ValidateTimerStartTime(ref GameConfig defaultConfig, ref GameConfig config)
        {
            if (config.TimerStartTime < 0)
            {
                Debug.LogError($"Mod: {config.ModName} TimerStartTime is less than 0. Setting from default config. Value: {defaultConfig.TimeBeforeTimerTrigger}");
                config.TimerStartTime = defaultConfig.TimerStartTime;
            }
        }

        private string GetFilePath(string filePath, string defaultPath, string modName)
        {
            var fullPath = Path.Combine(_modsPath, modName, filePath);
            if (!File.Exists(fullPath))
            {
                Debug.LogError($"Mod: {modName} File not found. Setting from default config path: {fullPath}");
                return defaultPath;
            }

            return fullPath;
        }

        private void Save(ref GameConfig gameConfig)
        {
            Debug.LogError($"SAVING MOD CONFIG {gameConfig.SuikaDropChances.Length}");
            
            var json = JsonConvert.SerializeObject(gameConfig);
            var filePath = Path.Combine(gameConfig.ModName, "config.json");

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(json);
            }
        }
    }
}