using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Factories
{
    public class ContainerFactory : MonoBehaviour
    {
        [SerializeField] private GameObject containerPrefab;
        [SerializeField] private Transform spawnPoint;
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private ResetService _resetService;
        [Inject] private IConfigLoader _configLoader;

        public async UniTask Create()
        {
            var container = _objectResolver.Instantiate(containerPrefab, spawnPoint.position, Quaternion.identity);
            var imagePath = _configLoader.CurrentConfig.ContainerImagePath;
            
            if (!_configLoader.IsDefault)
            {
                var sprite = await SpriteHelper.CreateSprite(imagePath, StaticData.ContainerSpriteSize, StaticData.ContainerSpriteSize);
                container.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            }
            else
            {
                var sprite = await SpriteHelper.CreateSpriteFromStreamingAssests(imagePath, StaticData.ContainerSpriteSize, StaticData.ContainerSpriteSize);
                container.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            }

            _resetService.SetContainer(container);
        }
    }
}