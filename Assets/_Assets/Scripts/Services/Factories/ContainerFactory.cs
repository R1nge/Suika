using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.StateMachine;
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

        public void Create()
        {
            var container = _objectResolver.Instantiate(containerPrefab, spawnPoint.position, Quaternion.identity);
            var imagePath = _configLoader.CurrentConfig.ContainerImagePath;
            container.GetComponentInChildren<SpriteRenderer>().sprite = SpriteHelper.CreateSprite(imagePath, 128, 128);
            _resetService.SetContainer(container);
        }
    }
}