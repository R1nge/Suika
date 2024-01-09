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
        private Rigidbody2D _suikaRigidbody;
        private bool _canDrop = true;
        private YieldInstruction _wait = new WaitForSeconds(1f);

        public void SpawnSuika() => Spawn();

        public void SpawnContinue() => _suikaRigidbody = _suikasFactory.CreatePlayerContinue(transform.position, transform);

        private void Update()
        {
#if UNITY_ANDROID
            if (Input.GetMouseButtonUp(0) && _canDrop && _playerInput.Enabled)
            {
                Drop();
                StartCoroutine(Cooldown());
            }

#else
             if (Input.GetMouseButtonDown(0) && _canDrop && _playerInput.Enabled)
            {
                Drop();
                StartCoroutine(Cooldown());
            }
#endif
        }

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
    }
}