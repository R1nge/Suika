using UnityEngine;

namespace _Assets.Scripts.Services.Factories
{
    public class ContainerFactory : MonoBehaviour
    {
        [SerializeField] private GameObject containerPrefab;
        [SerializeField] private Transform spawnPoint;
        
        public GameObject Create()
        {
            var container = Instantiate(containerPrefab, spawnPoint.position, Quaternion.identity);
            return container;
        }
    }
}