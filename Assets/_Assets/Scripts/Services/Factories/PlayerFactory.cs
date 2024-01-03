using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Factories
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private ResetService _resetService;

        public GameObject Create()
        {
            var player = _objectResolver.Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            _resetService.SetPlayer(player);
            return player;
        }
    }
}