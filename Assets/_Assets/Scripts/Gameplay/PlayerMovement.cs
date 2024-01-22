using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using VContainer;
using PlayerInput = _Assets.Scripts.Services.PlayerInput;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputActionAsset controls;
        [SerializeField] private float horizontalLimit = 2.65f;
        [Inject] private PlayerInput _playerInput;
        private InputAction _moveVector;
        

        private void OnEnable() => controls.Enable();

        private void Awake() => _moveVector = controls.FindActionMap("Game").FindAction("Move");

        private void Update()
        {
            if (!_playerInput.Enabled) return;

            var input = Mathf.Clamp(_moveVector.ReadValue<Vector2>().x, -horizontalLimit, horizontalLimit);
            var newPosition = transform.position + new Vector3(input, 0, 0);

            
            //Probably, should be in the input class and
            //return a vector3 depending on the device being used
            //but I don't care
            if (_playerInput.LastUsedDevice.name == "Mouse")
            {
                Debug.LogError("Mouse");
                input = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Mouse.current.position.value).x, -horizontalLimit, horizontalLimit);
                newPosition = new Vector3(input, transform.position.y, transform.position.z);
            }
            else if (_playerInput.LastUsedDevice.name == "Touchscreen")
            {
                Debug.LogError("Touch");
                input = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.value).x, -horizontalLimit, horizontalLimit);
                newPosition = new Vector3(input, transform.position.y, transform.position.z);
            }


            var clamped = Mathf.Clamp(newPosition.x, -horizontalLimit, horizontalLimit);
            transform.position = new Vector3(clamped, transform.position.y, transform.position.z);
        }

        private void OnDisable() => controls.Disable();
    }
}