using UnityEngine;

namespace _Assets.Scripts.Services.Factories
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;

        public GameObject Create()
        {
            var player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            return player;
        }
    }
}