using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Services.UIs
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Button mainMenu;
        [SerializeField] private Button restart;

        private void Awake()
        {
            mainMenu.onClick.AddListener(ShowMainMenu);
            restart.onClick.AddListener(Restart);
        }

        private void ShowMainMenu()
        {
            //TODO: destroy everything
            //TODO: show main menu
        }

        private void Restart()
        {
            //TODO: restart game
            //Show game UI
            //Destroy suikas
        }
    }
}