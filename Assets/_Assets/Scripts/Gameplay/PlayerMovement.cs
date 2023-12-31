using _Assets.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float horizontalLimit = 4;
        [SerializeField] private float speed;
        [Inject] private PlayerInput _playerInput;

        private void Update()
        {
            if (!_playerInput.Enabled) return;
            var position = new Vector3(Input.GetAxis("Horizontal"), 0, 0) * (Time.deltaTime * speed);
            var finalPosition = Mathf.Clamp(transform.position.x + position.x, -horizontalLimit, horizontalLimit);
            transform.position = new Vector3(finalPosition, transform.position.y, transform.position.z);
        }
    }
}