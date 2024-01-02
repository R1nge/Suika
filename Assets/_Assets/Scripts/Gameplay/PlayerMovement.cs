using _Assets.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float horizontalLimit = 4;
        [Inject] private PlayerInput _playerInput;
        private Camera _camera;

        private void Awake() => _camera = Camera.main;

        private void Update()
        {
            if (!_playerInput.Enabled) return;
            var position = _camera.ScreenToWorldPoint(Input.mousePosition);
            var finalPosition = Mathf.Clamp(position.x, -horizontalLimit, horizontalLimit);
            transform.position = new Vector3(finalPosition, transform.position.y, transform.position.z);
        }
    }
}