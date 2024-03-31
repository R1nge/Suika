using _Assets.Scripts.Services.GameModes;
using UnityEngine;

namespace _Assets.Scripts.Services.Configs
{
    public class ConfigProvider : MonoBehaviour
    {
        [SerializeField] private UIConfig uiConfig;
        [SerializeField] private SuikasConfig suikasConfig;
        [SerializeField] private UITheme lightTheme;
        [SerializeField] private UITheme darkTheme;
        [SerializeField] private GameModeConfig[] gameModeConfigs;
        public UIConfig UIConfig => uiConfig;
        public SuikasConfig SuikasConfig => suikasConfig;
        public UITheme LightTheme => lightTheme;
        public UITheme DarkTheme => darkTheme;
        public GameModeConfig[] GameModeConfigs => gameModeConfigs;
    }
}