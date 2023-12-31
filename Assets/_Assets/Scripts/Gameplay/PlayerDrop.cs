using System.Collections;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.UIs;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerDrop : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [Inject] private SuikasFactory _suikasFactory;
        [Inject] private SuikaDataProvider _suikaDataProvider;
        private readonly YieldInstruction _wait = new WaitForSeconds(.5f);
        private bool _canDrop = true;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _canDrop)
            {
                Drop();
                UpdateSprite();
            }
        }

        private void Drop()
        {
            StartCoroutine(DropCooldown());
            _suikasFactory.Create(transform.position);
        }

        private void UpdateSprite() => spriteRenderer.sprite = _suikaDataProvider.GetCurrentSuika();

        private IEnumerator DropCooldown()
        {
            _canDrop = false;
            yield return _wait;
            _canDrop = true;
        }
    }
}