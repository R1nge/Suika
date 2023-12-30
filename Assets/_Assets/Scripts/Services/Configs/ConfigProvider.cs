using UnityEngine;

namespace _Assets.Scripts.Services.Configs
{
    public class ConfigProvider : MonoBehaviour
    {
        [SerializeField] private UIConfig uiConfig;
        [SerializeField] private SuikasConfig suikasConfig;
        public UIConfig UIConfig => uiConfig;
        public SuikasConfig SuikasConfig => suikasConfig;
    }
}