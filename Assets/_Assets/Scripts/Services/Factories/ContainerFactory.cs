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
        
        
        public GameObject Create()
        {
            var container = _objectResolver.Instantiate(containerPrefab, spawnPoint.position, Quaternion.identity);
            return container;
        }
    }
}