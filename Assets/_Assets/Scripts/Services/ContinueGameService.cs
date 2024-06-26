﻿using System;
using System.Collections.Generic;
using System.IO;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.GameModes;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Assets.Scripts.Services
{
    public class ContinueGameService
    {
        private ContinueData _continueData;
        private readonly List<Suika> _suikas = new();
        private readonly AudioService _audioService;
        private readonly SuikasFactory _suikasFactory;
        private readonly RandomNumberGenerator _randomNumberGenerator;
        private readonly ScoreService _scoreService;
        private readonly TimeRushTimer _timeRushTimer;
        private readonly GameModeService _gameModeService;

        public bool HasData => _continueData != null;

        private ContinueGameService(AudioService audioService, SuikasFactory suikasFactory,
            RandomNumberGenerator randomNumberGenerator, ScoreService scoreService, TimeRushTimer timeRushTimer, GameModeService gameModeService)
        {
            _audioService = audioService;
            _suikasFactory = suikasFactory;
            _randomNumberGenerator = randomNumberGenerator;
            _scoreService = scoreService;
            _timeRushTimer = timeRushTimer;
            _gameModeService = gameModeService;
        }

        public async void Continue()
        {
            await Load();

            Debug.LogError($"Song index: {_continueData.SongIndex}");

            for (int i = 0; i < _continueData.SuikasContinueData.Count; i++)
            {
                var position = new Vector3(_continueData.SuikasContinueData[i].PositionX,
                    _continueData.SuikasContinueData[i].PositionY, 0);
                _suikasFactory.CreateContinue(_continueData.SuikasContinueData[i].Index, position);
            }

            _randomNumberGenerator.SetCurrent(_continueData.CurrentSuikaIndex);
            _randomNumberGenerator.SetNext(_continueData.NextSuikaIndex);

            _timeRushTimer.CurrentTime = _continueData.TimeRushTime;
            _gameModeService.SelectedGameMode = _continueData.GameMode;
        }

        public void UpdateScore()
        {
            _scoreService.AddScore(_continueData.Score);
        }

        public async UniTask Load()
        {
            var dataFolderInfo = new DirectoryInfo(PathsHelper.DataPath);

            if (!dataFolderInfo.Exists)
            {
                dataFolderInfo.Create();
                return;
            }

            foreach (var fileInfo in dataFolderInfo.GetFiles(PathsHelper.ContinueDataJson))
            {
                var reader = new StreamReader(fileInfo.FullName);

                try
                {
                    var json = await reader.ReadToEndAsync();
                    _continueData = JsonConvert.DeserializeObject<ContinueData>(json);
                    reader.Close();
                    reader.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    reader.Close();
                    reader.Dispose();
                    throw;
                }
            }
        }

        public void AddSuika(Suika suika) => _suikas.Add(suika);

        public void RemoveSuika(Suika suika) => _suikas.Remove(suika);

        public async UniTask Save()
        {
            _continueData = new ContinueData(_audioService.LastSongIndex, new List<ContinueData.SuikaContinueData>(),
                _randomNumberGenerator.Current, _randomNumberGenerator.Next, _scoreService.Score,
                _timeRushTimer.CurrentTime,_gameModeService.SelectedGameMode);

            _continueData.SuikasContinueData = new List<ContinueData.SuikaContinueData>(_suikas.Count);
            for (int i = 0; i < _suikas.Count; i++)
            {
                if (_suikas[i] == null)
                {
                    continue;
                }

                var index = _suikas[i].Index;
                var position = _suikas[i].transform.position;
                _continueData.SuikasContinueData.Add(new ContinueData.SuikaContinueData(index, position.x, position.y));
            }

            _continueData.SongIndex = _audioService.LastSongIndex;

            _continueData.CurrentSuikaIndex = _randomNumberGenerator.Current;
            _continueData.NextSuikaIndex = _randomNumberGenerator.Next;

            _continueData.Score = _scoreService.Score;

            _continueData.TimeRushTime = _timeRushTimer.CurrentTime;
            
            _continueData.GameMode = _gameModeService.SelectedGameMode;

            var path = Path.Combine(PathsHelper.DataPath, PathsHelper.ContinueDataJson);
            var json = JsonConvert.SerializeObject(_continueData);
            await File.WriteAllTextAsync(path, json);
        }

        public void DeleteContinueData()
        {
            _continueData = null;
            var path = Path.Combine(PathsHelper.DataPath, PathsHelper.ContinueDataJson);
            File.Delete(path);
        }
    }
}