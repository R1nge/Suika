using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.StateMachine;
using Cysharp.Threading.Tasks;
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
        private readonly AudioService _audioService;
        private readonly SpritesCacheService _spritesCacheService;
        private readonly VibrationService _vibrationService;

        private SuikasFactory(IObjectResolver objectResolver, ConfigProvider configProvider, RandomNumberGenerator randomNumberGenerator, ScoreService scoreService, ResetService resetService, AudioService audioService, SpritesCacheService spritesCacheService, VibrationService vibrationService)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
            _randomNumberGenerator = randomNumberGenerator;
            _scoreService = scoreService;
            _resetService = resetService;
            _audioService = audioService;
            _spritesCacheService = spritesCacheService;
            _vibrationService = vibrationService;
        }

        public Rigidbody2D CreateKinematic(Vector3 position, Transform parent)
        {
            var index = _randomNumberGenerator.PickRandomSuika();

            var suikaPrefab = _configProvider.SuikasConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suikaPrefab.gameObject, position, Quaternion.identity, parent).GetComponent<Suika>();
            suikaInstance.SetIndex(index);

            var rigidbody = suikaInstance.GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;


            var sprite = _spritesCacheService.GetSuikaSprite(index);
            suikaInstance.SetSprite(sprite);

            AddToResetService(suikaInstance);
            AddPolygonCollider(suikaInstance);

            return rigidbody;
        }

        public void Create(int index, Vector3 position)
        {
            index++;

            if (!_configProvider.SuikasConfig.HasPrefab(index))
            {
                AddScore(index--);
                Debug.LogError($"SuikasFactory: Index is out of range {index}");
                return;
            }

            var suikaPrefab = _configProvider.SuikasConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suikaPrefab.gameObject, position, Quaternion.identity)
                .GetComponent<Suika>();
            suikaInstance.SetIndex(index);
            suikaInstance.Drop();

            var sprite = _spritesCacheService.GetSuikaSprite(index);
            suikaInstance.SetSprite(sprite);

            AddScore(index);
            AddToResetService(suikaInstance);
            AddPolygonCollider(suikaInstance);
            _audioService.PlayMerge(index).Forget();
            _audioService.PlaySong(index).Forget();
            _vibrationService.Vibrate();
        }

        public void CreateContinue(int index, Vector3 position)
        {
            var suikaPrefab = _configProvider.SuikasConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suikaPrefab.gameObject, position, Quaternion.identity).GetComponent<Suika>();
            suikaInstance.SetIndex(index);
            suikaInstance.Drop();
            suikaInstance.Land();

            var sprite = _spritesCacheService.GetSuikaSprite(index);
            suikaInstance.SetSprite(sprite);
            
            AddToResetService(suikaInstance);
            AddPolygonCollider(suikaInstance);
        }

        public Rigidbody2D CreatePlayerContinue(Vector3 position, Transform parent)
        {
            var index = _randomNumberGenerator.Current;
            var suikaPrefab = _configProvider.SuikasConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suikaPrefab.gameObject, position, Quaternion.identity, parent).GetComponent<Suika>();
            suikaInstance.SetIndex(index);

            var sprite = _spritesCacheService.GetSuikaSprite(index);
            suikaInstance.SetSprite(sprite);
            
            AddToResetService(suikaInstance);
            AddPolygonCollider(suikaInstance);
            
            var rigidbody2D = suikaInstance.GetComponent<Rigidbody2D>();
            rigidbody2D.isKinematic = true;

            return rigidbody2D;
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

        private void AddPolygonCollider(Suika suika)
        {
            //Causing lag spikes
            //It's not a noticeable lag, so ignoring it for now
            var collider = suika.SpriteRenderer.gameObject.AddComponent<PolygonCollider2D>();
            var optimizer = suika.PolygonColliderOptimizer;
            optimizer.GetInitPaths(collider);
            optimizer.OptimizePolygonCollider(.01f);
        }
    }
}