using UnityEngine;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private Canvas inGameCanvas;

        private void Awake()
        {
            inGameCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            inGameCanvas.worldCamera = Camera.main;
            inGameCanvas.sortingOrder = -99;
            inGameCanvas.planeDistance = 10;
        }
    }
}