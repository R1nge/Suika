using UnityEngine;

namespace _Assets.Scripts.Services.Configs
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI")]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private GameObject loadingCurtain;
        [SerializeField] private GameObject modsMenu;
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject selectGameModeMenu;
        [SerializeField] private GameObject inGameMenu;
        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private GameObject settingsMenu;
        [SerializeField] private GameObject pauseMenu;
        public GameObject LoadingCurtain => loadingCurtain;
        public GameObject ModsMenu => modsMenu;
        public GameObject MainMenu => mainMenu;
        public GameObject SelectGameModeMenu => selectGameModeMenu;
        public GameObject InGameMenu => inGameMenu;
        public GameObject GameOverMenu => gameOverMenu;
        public GameObject SettingMenu => settingsMenu;
        public GameObject PauseMenu => pauseMenu;
    }
}