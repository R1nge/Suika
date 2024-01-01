using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Datas.GameConfigs;
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

        public Sprite GetCurrentSuika()
        {
            var current = _randomNumberGenerator.Current;
            var path = _configLoader.CurrentConfig.SuikaIconsPaths[current];
            const int size = 128;
            return SpriteHelper.CreateSprite(path, size, size);
        }

        public Sprite GetNextSuika()
        {
            var next = _randomNumberGenerator.Next;
            var path = _configLoader.CurrentConfig.SuikaIconsPaths[next];
            const int size = 128;
            return SpriteHelper.CreateSprite(path, size, size);
        }
    }
}