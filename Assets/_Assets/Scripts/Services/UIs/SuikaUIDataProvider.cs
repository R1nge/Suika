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
        private readonly SpriteCreator _spriteCreator;


        private SuikaUIDataProvider(RandomNumberGenerator randomNumberGenerator, SpriteCreator spriteCreator)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _spriteCreator = spriteCreator;
        }

        public async UniTask<Sprite> GetCurrentSuika()
        {
            var current = _randomNumberGenerator.Current;
            var sprite = await _spriteCreator.CreateSuikaIconSprite(current);
            return sprite;
        }


        public async UniTask<Sprite> GetNextSuika()
        {
            var next = _randomNumberGenerator.Next;
            var sprite = await _spriteCreator.CreateSuikaIconSprite(next);
            return sprite;
        }
    }
}