using System.Collections.Generic;
using System.IO;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Factories;
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

        public bool HasData => _continueData.SuikasContinueData != null;

        private ContinueGameService(AudioService audioService, SuikasFactory suikasFactory,
            RandomNumberGenerator randomNumberGenerator, ScoreService scoreService)
        {
            _audioService = audioService;
            _suikasFactory = suikasFactory;
            _randomNumberGenerator = randomNumberGenerator;
            _scoreService = scoreService;
        }

        public async void Continue()
        {
            await Load();

            _audioService.PlaySong(_continueData.SongIndex).Forget();

            for (int i = 0; i < _continueData.SuikasContinueData.Count; i++)
            {
                var position = new Vector3(_continueData.SuikasContinueData[i].PositionX, _continueData.SuikasContinueData[i].PositionY, 0);
                _suikasFactory.CreateContinue(_continueData.SuikasContinueData[i].Index, position);
            }

            _randomNumberGenerator.SetCurrent(_continueData.CurrentSuikaIndex);
            _randomNumberGenerator.SetNext(_continueData.NextSuikaIndex);
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

            foreach (var fileInfo in dataFolderInfo.GetFiles("continueData.json"))
            {
                var reader = new StreamReader(fileInfo.FullName);
                var json = await reader.ReadToEndAsync();
                _continueData = JsonConvert.DeserializeObject<ContinueData>(json);
                reader.Close();
                reader.Dispose();
            }
        }

        public void AddSuika(Suika suika) => _suikas.Add(suika);

        public void RemoveSuika(Suika suika) => _suikas.Remove(suika);

        public void Save()
        {
            _continueData.SuikasContinueData = new List<ContinueData.SuikaContinueData>(_suikas.Count);
            for (int i = 0; i < _suikas.Count; i++)
            {
                var index = _suikas[i].Index;
                var position = _suikas[i].transform.position;
                _continueData.SuikasContinueData.Add(new ContinueData.SuikaContinueData(index, position.x, position.y));
            }

            _continueData.SongIndex = _audioService.LastSongIndex;
            
            _continueData.CurrentSuikaIndex = _randomNumberGenerator.Current;
            _continueData.NextSuikaIndex = _randomNumberGenerator.Next;

            _continueData.Score = _scoreService.Score;

            var path = Path.Combine(PathsHelper.DataPath, "continueData.json");
            var json = $"{_continueData.GetHashCode()}\n" + JsonConvert.SerializeObject(_continueData);
            File.WriteAllText(path, json);
        }
    }
}