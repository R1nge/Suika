namespace _Assets.Scripts.Services
{
    public class PlayerInput
    {
        private bool _enabled;
        public bool Enabled => _enabled;
        
        public void Enable() => _enabled = true;
        public void Disable() => _enabled = false;
    }
}