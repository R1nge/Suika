using UnityEngine;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private Canvas inGameCanvas;
        [SerializeField] private int sortingOrder;

        private void Start()
        {
            inGameCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            inGameCanvas.worldCamera = Camera.main;
            inGameCanvas.sortingOrder = sortingOrder;
            inGameCanvas.planeDistance = 10;
#if !UNITY_ANDROID
            Camera.main.SetTargetBuffers(Display.main.colorBuffer, Display.main.depthBuffer); 
#endif
        }
    }
}