using System.Collections;
using _Assets.Scripts.Services.Factories;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerDrop : MonoBehaviour
    {
        [Inject] private SuikasFactory _suikasFactory;
        private readonly YieldInstruction _wait = new WaitForSeconds(.25f);
        private bool _canDrop = true;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _canDrop)
            {
                StartCoroutine(Wait());
                _suikasFactory.Create(transform.position);
            }
        }

        private IEnumerator Wait()
        {
            _canDrop = false;
            yield return _wait;
            _canDrop = true;
        }
    }
}