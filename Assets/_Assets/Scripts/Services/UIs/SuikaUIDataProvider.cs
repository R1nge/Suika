using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs
{
    public class SuikaUIDataProvider
    {
        private readonly RandomNumberGenerator _randomNumberGenerator;
        private readonly SpritesCacheService _spritesCacheService;


        private SuikaUIDataProvider(RandomNumberGenerator randomNumberGenerator, SpritesCacheService spritesCacheService)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _spritesCacheService = spritesCacheService;
        }

        public Sprite GetCurrentSuika()
        {
            var current = _randomNumberGenerator.Current;
            var sprite = _spritesCacheService.GetSuikaIconSprite(current);
            return sprite;
        }
        
        public Sprite GetNextSuika()
        {
            var next = _randomNumberGenerator.Next;
            var sprite = _spritesCacheService.GetSuikaIconSprite(next);
            return sprite;
        }
    }
}