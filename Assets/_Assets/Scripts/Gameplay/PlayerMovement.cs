using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using VContainer;
using PlayerInput = _Assets.Scripts.Services.PlayerInput;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputActionAsset controls;
        [SerializeField] private float horizontalLimit = 4;
        [Inject] private PlayerInput _playerInput;
        private InputAction _moveVector;
        private InputDevice m_LastUsedDevice;

        private void OnEnable()
        {
            controls.Enable();
            InputSystem.onEvent += OnInputSystemEvent;
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (m_LastUsedDevice == device)
                return;
 
            m_LastUsedDevice = device;
            //LastUsedDeviceChanged?.Invoke(0);
        }


        private void OnInputSystemEvent(InputEventPtr eventPtr, InputDevice device)
        {
            if (m_LastUsedDevice == device)
                return;
 
            // Some devices like to spam events like crazy.
            // Example: PS4 controller on PC keeps triggering events without meaningful change.
            var eventType = eventPtr.type;
            if (eventType == StateEvent.Type) {
                // Go through the changed controls in the event and look for ones actuated
                // above a magnitude of a little above zero.
                if (!eventPtr.EnumerateChangedControls(device: device, magnitudeThreshold: 0.0001f).Any())
                    return;
            }
 
 
            m_LastUsedDevice = device;
            //LastUsedDeviceChanged?.Invoke(0);
        }

        private void Awake() => _moveVector = controls.FindActionMap("Game").FindAction("Move");

        private void Update()
        {
            if (!_playerInput.Enabled) return;
            
            var input = Mathf.Clamp(_moveVector.ReadValue<Vector2>().x, -horizontalLimit, horizontalLimit);
            var newPosition = transform.position + new Vector3(input, 0, 0);
            
            if (m_LastUsedDevice.name == "Mouse")
            {
                Debug.LogError("Mouse");
                input = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Mouse.current.position.value).x, -horizontalLimit, horizontalLimit);
                newPosition = new Vector3(input, transform.position.y, transform.position.z);
            }
            
            
            var clamped = Mathf.Clamp(newPosition.x, -horizontalLimit, horizontalLimit);
            transform.position = new Vector3(clamped, transform.position.y, transform.position.z);
        }

        private void OnDisable() => controls.Disable();
    }
}