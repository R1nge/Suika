using _Assets.Scripts.Services.GameModes;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class SelectGameModeUI : MonoBehaviour
    {
        [SerializeField] private Button classicButton;
        [SerializeField] private Button timeRushButton;
        [Inject] private GameModeService _gameModeService;

        private void Start()
        {
            classicButton.onClick.AddListener(Classic);
            timeRushButton.onClick.AddListener(TimeRush);
        }

        private void Classic() => _gameModeService.SelectGameMode(GameModeService.GameMode.Classic);

        private void TimeRush() => _gameModeService.SelectGameMode(GameModeService.GameMode.TimeRush);

        private void OnDestroy()
        {
            classicButton.onClick.RemoveListener(Classic);
            timeRushButton.onClick.RemoveListener(TimeRush);
        }
    }
}