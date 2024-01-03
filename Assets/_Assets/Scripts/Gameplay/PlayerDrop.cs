using System.Collections;
using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs;
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

        public void SpawnSuika() => Spawn();

        private void Update()
        {
#if UNITY_ANDROID
            if (Input.GetMouseButtonUp(0) && _playerInput.Enabled)
            {
                if (_canDrop)
                {
                    Drop();
                    StartCoroutine(Cooldown());
                }
            }

#else
             if (Input.GetMouseButtonDown(0) && _canDrop && _playerInput.Enabled)
            {
                Drop();
                StartCoroutine(Cooldown());
            }
#endif
        }

        private async void Spawn()
        {
            _suikaRigidbody = await _suikasFactory.CreateKinematic(transform.position, transform);
        }

        private void Drop()
        {
            if (_suikaRigidbody != null)
            {
                _suikaRigidbody.transform.parent = null;
                _suikaRigidbody.isKinematic = false;
                _suikaRigidbody = null;
            }
        }

        private IEnumerator Cooldown()
        {
            _canDrop = false;
            yield return _wait;

            if (_suikaRigidbody == null)
            {
                Spawn();
            }

            _canDrop = true;
        }
    }
}