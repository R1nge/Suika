using System;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Datas.GameConfigs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs
{
    public class SuikaUIDataProvider
    {
        private readonly RandomNumberGenerator _randomNumberGenerator;
        private readonly IConfigLoader _configLoader;


        private SuikaUIDataProvider(RandomNumberGenerator randomNumberGenerator, IConfigLoader configLoader)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _configLoader = configLoader;
        }

        public async UniTask<Sprite> GetCurrentSuika()
        {
            var current = _randomNumberGenerator.Current;
            if (!_configLoader.IsDefault)
            {
                var path = _configLoader.CurrentConfig.SuikaIconsPaths[current];
                var size = StaticData.SuikaIconSpriteSize;
                return await SpriteHelper.CreateSprite(path, size, size);
            }
            else
            {
                var path = _configLoader.CurrentConfig.SuikaIconsPaths[current];
                var sprite = await SpriteHelper.CreateSpriteFromStreamingAssests(path, StaticData.SuikaIconSpriteSize,
                    StaticData.SuikaIconSpriteSize);
                return sprite;
            }
        }

        public async UniTask<Sprite> GetNextSuika()
        {
            var next = _randomNumberGenerator.Next;
            var size = StaticData.SuikaIconSpriteSize;
            if (!_configLoader.IsDefault)
            {
                var path = _configLoader.CurrentConfig.SuikaIconsPaths[next];

                return await SpriteHelper.CreateSprite(path, size, size);
            }
            else
            {
                var path = _configLoader.CurrentConfig.SuikaIconsPaths[next];
                return await SpriteHelper.CreateSpriteFromStreamingAssests(path, size, size);
            }
        }
    }
}