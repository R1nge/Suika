using UnityEngine;

namespace _Assets.Scripts.Services.GameModes
{
    [CreateAssetMenu(fileName = "GameMode", menuName = "Configs/GameMode")]
    public class GameModeConfig : ScriptableObject
    {
        [SerializeField] private GameModeService.GameMode gameMode;
        [SerializeField] private string title;
        [SerializeField] private string description;
        public GameModeService.GameMode GameMode => gameMode;
        public string Title => title;
        public string Description => description;
    }
}