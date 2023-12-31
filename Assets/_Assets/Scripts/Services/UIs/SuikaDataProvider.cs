using _Assets.Scripts.Services.Configs;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs
{
    public class SuikaDataProvider
    {
        private readonly RandomNumberGenerator _randomNumberGenerator;
        private readonly ConfigProvider _configProvider;

        private SuikaDataProvider(RandomNumberGenerator randomNumberGenerator, ConfigProvider configProvider)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _configProvider = configProvider;
        }

        public Sprite GetCurrentSuika()
        {
            var current = _randomNumberGenerator.Current;
            return _configProvider.SuikasConfig.GetIcon(current);
        }

        public Sprite GetNextSuika()
        {
            var next = _randomNumberGenerator.Next;
            return _configProvider.SuikasConfig.GetIcon(next);
        }
    }
}