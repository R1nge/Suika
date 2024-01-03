using UnityEngine;

namespace _Assets.Scripts.Services.UIs
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private Transform suika;
        [SerializeField] private float suikaRotationSpeed;

        private void Update() => suika.Rotate(Vector3.forward * (suikaRotationSpeed * Time.deltaTime));
        
        public void Show()
        {
            
        }

        public void Hide()
        {
            //TODO: animation?
            
            
            Destroy(gameObject);
        }
    }
}