using _Assets.Scripts.Misc;
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
        [Inject] private SpriteCreator _spriteCreator;

        public async UniTask Create()
        {
            var container = _objectResolver.Instantiate(containerPrefab, spawnPoint.position, Quaternion.identity);

            var sprite = await _spriteCreator.CreateContainerSprite();
            container.GetComponentInChildren<SpriteRenderer>().sprite = sprite;

            _resetService.SetContainer(container);
        }
    }
}