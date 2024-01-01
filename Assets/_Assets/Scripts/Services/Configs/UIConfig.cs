﻿using UnityEngine;

namespace _Assets.Scripts.Services.Configs
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI")]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private GameObject loadingMenu;
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject modsMenu;
        [SerializeField] private GameObject inGameMenu;
        [SerializeField] private GameObject gameOverMenu;
        public GameObject LoadingMenu => loadingMenu;
        public GameObject MainMenu => mainMenu;
        public GameObject ModsMenu => modsMenu;
        public GameObject InGameMenu => inGameMenu;
        public GameObject GameOverMenu => gameOverMenu;
    }
}