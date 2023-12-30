using _Assets.Scripts.Services.Factories;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerDrop : MonoBehaviour
    {
        [Inject] private SuikasFactory _suikasFactory;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _suikasFactory.CreateWithZeroScale(transform.position);
            }
        }
    }
}