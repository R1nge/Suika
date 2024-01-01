using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.StateMachine;
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
        private readonly ScoreService _scoreService;
        private readonly ResetService _resetService;
        private readonly IConfigLoader _configLoader;

        private SuikasFactory(IObjectResolver objectResolver, ConfigProvider configProvider,
            RandomNumberGenerator randomNumberGenerator, ScoreService scoreService, ResetService resetService, IConfigLoader configLoader)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
            _randomNumberGenerator = randomNumberGenerator;
            _scoreService = scoreService;
            _resetService = resetService;
            _configLoader = configLoader;
        }

        public Rigidbody2D CreateKinematic(Vector3 position, Transform parent)
        {
            var index = _randomNumberGenerator.PickRandomSuika();
            var chance = _configLoader.CurrentConfig.SuikaDropChances[index];
            
            if (Random.Range(0, 1f) > chance)
            {
                return CreateKinematic(position, parent);
            }

            var suikaPrefab = _configProvider.SuikasConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suikaPrefab.gameObject, position, Quaternion.identity, parent).GetComponent<Suika>();
            suikaInstance.SetIndex(index);
            var rigidbody = suikaInstance.GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;
            AddToResetService(suikaInstance);
            return rigidbody;
        }

        public void Create(int index, Vector3 position)
        {
            index++;

            if (!_configProvider.SuikasConfig.HasPrefab(index))
            {
                AddScore(index--);
                Debug.LogWarning($"SuikasFactory: Index is out of range {index}");
                return;
            }

            var suikaPrefab = _configProvider.SuikasConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suikaPrefab.gameObject, position, Quaternion.identity).GetComponent<Suika>();
            suikaInstance.SetIndex(index);
            AddScore(index);
            AddToResetService(suikaInstance);
        }

        private void AddScore(int index)
        {
            //Previous + level (index) + points? 
            var currentLevel = index;
            var previousPoints = _configProvider.SuikasConfig.GetPoints(Mathf.Clamp(index - 1, 0, 1000));
            var totalPoints = currentLevel + previousPoints;
            _scoreService.AddScore(totalPoints);
        }

        private void AddToResetService(Suika suika) => _resetService.AddSuika(suika);
    }
}