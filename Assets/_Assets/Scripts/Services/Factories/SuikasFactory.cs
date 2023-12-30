using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Factories
{
    public class SuikasFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ConfigProvider _configProvider;
        private readonly RandomNumberGenerator _randomNumberGenerator;

        private SuikasFactory(IObjectResolver objectResolver, ConfigProvider configProvider,
            RandomNumberGenerator randomNumberGenerator)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
            _randomNumberGenerator = randomNumberGenerator;
        }

        public Suika Create(Vector3 position)
        {
            var index = _randomNumberGenerator.PickRandomSuika();
            var suika = _configProvider.SuikasConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suika.gameObject, position, Quaternion.identity);
            var ss = suikaInstance.GetComponent<Suika>();
            ss.SetIndex(index);
            return ss;
        }
        
        public Suika CreateWithZeroScale(Vector3 position)
        {
            var index = _randomNumberGenerator.PickRandomSuika();
            var suika = _configProvider.SuikasConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suika.gameObject, position, Quaternion.identity).GetComponent<Suika>();
            suikaInstance.SetIndex(index);
            return suikaInstance;
        }

        public Suika CreateWithZeroScale(int index, Vector3 position)
        {
            index++;
            var suika = _configProvider.SuikasConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suika.gameObject, position, Quaternion.identity).GetComponent<Suika>();
            suikaInstance.SetIndex(index);
            return suikaInstance;
        }
    }
}