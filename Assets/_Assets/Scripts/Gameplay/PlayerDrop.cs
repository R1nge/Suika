using System.Collections;
using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerDrop : MonoBehaviour
    {
        [Inject] private SuikasFactory _suikasFactory;
        [Inject] private SuikaUIDataProvider _suikaUIDataProvider;
        [Inject] private PlayerInput _playerInput;
        private readonly YieldInstruction _wait = new WaitForSeconds(1f);
        private Rigidbody2D _suikaRigidbody;
        private bool _canDrop = true;

        public void SpawnSuika() => Spawn().Forget();

        private void Update()
        {
#if UNITY_ANDROID
            if (Input.GetMouseButtonUp(0) && _canDrop && _playerInput.Enabled)
            {
                Drop();
                Cooldown().Forget();
            }

#else
             if (Input.GetMouseButtonDown(0) && _canDrop && _playerInput.Enabled)
            {
                Drop();
                Cooldown().Forget();
            }
#endif
        }

        private async UniTask Spawn()
        {
            _suikaRigidbody = await _suikasFactory.CreateKinematic(transform.position, transform);
        }

        private void Drop()
        {
            _suikaRigidbody.transform.parent = null;
            _suikaRigidbody.isKinematic = false;
            _suikaRigidbody.GetComponent<Suika>().Drop();
            _suikaRigidbody = null;
        }

        private async UniTask Cooldown()
        {
            _canDrop = false;
            await UniTask.Delay(1000);
            await Spawn();
            _canDrop = true;
        }
    }
}