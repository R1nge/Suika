using System.Collections;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using PlayerInput = _Assets.Scripts.Services.PlayerInput;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerDrop : MonoBehaviour
    {
        [Inject] private SuikasFactory _suikasFactory;
        [Inject] private SuikaUIDataProvider _suikaUIDataProvider;
        [Inject] private PlayerInput _playerInput;
        private Rigidbody2D _suikaRigidbody;
        private bool _canDrop = true;
        private readonly YieldInstruction _wait = new WaitForSeconds(1f);

        private void Start() => _playerInput.OnDrop += Drop;

        private void Drop(InputAction.CallbackContext callback)
        {
            if (_canDrop && _playerInput.Enabled())
            {
                Drop();
                StartCoroutine(Cooldown());
            }
        }

        public void SpawnSuika() => Spawn();

        public void SpawnContinue() => _suikaRigidbody = _suikasFactory.CreatePlayerContinue(transform.position, transform);

        private void Spawn()
        {
            _suikaRigidbody = _suikasFactory.CreateKinematic(transform.position, transform);
        }

        private void Drop()
        {
            _suikaRigidbody.transform.parent = null;
            _suikaRigidbody.isKinematic = false;
            _suikaRigidbody.GetComponent<Suika>().Drop();
            _suikaRigidbody = null;
        }

        private IEnumerator Cooldown()
        {
            _canDrop = false;
            yield return _wait;
            Spawn();
            _canDrop = true;
        }

        private void OnDestroy() => _playerInput.OnDrop -= Drop;
    }
}