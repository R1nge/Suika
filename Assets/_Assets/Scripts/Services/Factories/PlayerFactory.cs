using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.StateMachine;
using Cysharp.Threading.Tasks;
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
        [Inject] private SpriteCreator _spriteCreator;

        public async UniTask<GameObject> Create()
        {
            var sprite = await _spriteCreator.CreatePlayerSkin();
            var player = _objectResolver.Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            player.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            player.GetComponent<PlayerController>().Init();
            _resetService.SetPlayer(player);
            return player;
        }
    }
}