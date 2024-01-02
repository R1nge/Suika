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
        private bool _canDrop = true;
        private Rigidbody2D _suikaRigidbody;

        private void Start() => Spawn();

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
            Debug.LogError("SPAWN");
        }

        private void Drop()
        {
            _suikaRigidbody.transform.parent = null;
            _suikaRigidbody.isKinematic = false;
            _suikaRigidbody = null;
        }

        private IEnumerator Cooldown()
        {
            _canDrop = false;
            yield return _wait;
            _canDrop = true;

            if (_suikaRigidbody == null)
            {
                Spawn();
            }
        }
    }
}