using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace _Assets.Scripts.Services
{
    public class PlayerInput
    {
        private bool _enabled;
        public bool Enabled => _enabled && !EventSystem.current.IsPointerOverGameObject();

        private InputDevice _lastUsedDevice;
        public InputDevice LastUsedDevice => _lastUsedDevice;

        public void Init()
        {
            //Since it's a singleton and init is called only once the game starts, we can forget about unsubscribing.
            InputSystem.onEvent += OnInputSystemEvent;
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        public void Enable() => _enabled = true;

        public void Disable() => _enabled = false;

        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (Equals(_lastUsedDevice, device))
                return;

            _lastUsedDevice = device;
            //LastUsedDeviceChanged?.Invoke(0);
        }


        private void OnInputSystemEvent(InputEventPtr eventPtr, InputDevice device)
        {
            if (_lastUsedDevice == device)
                return;

            // Some devices like to spam events like crazy.
            // Example: PS4 controller on PC keeps triggering events without meaningful change.
            var eventType = eventPtr.type;
            if (eventType == StateEvent.Type)
            {
                // Go through the changed controls in the event and look for ones actuated
                // above a magnitude of a little above zero.
                if (!eventPtr.EnumerateChangedControls(device: device, magnitudeThreshold: 0.0001f).Any())
                    return;
            }


            _lastUsedDevice = device;
            //LastUsedDeviceChanged?.Invoke(0);
        }
    }
}