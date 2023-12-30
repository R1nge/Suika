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

        public void Create(Vector3 position)
        {
            var index = _randomNumberGenerator.PickRandomSuika();
            var suikaPrefab = _configProvider.SuikasConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suikaPrefab.gameObject, position, Quaternion.identity).GetComponent<Suika>();
            suikaInstance.SetIndex(index);
        }

        public void Create(int index, Vector3 position)
        {
            index++;

            if (!_configProvider.SuikasConfig.HasPrefab(index))
            {
                Debug.LogWarning($"SuikasFactory: Index is out of range {index}");
                return;
            }

            var suikaPrefab = _configProvider.SuikasConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suikaPrefab.gameObject, position, Quaternion.identity).GetComponent<Suika>();
            suikaInstance.SetIndex(index);
        }
    }
}