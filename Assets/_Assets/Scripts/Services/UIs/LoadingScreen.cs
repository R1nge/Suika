using UnityEngine;

namespace _Assets.Scripts.Services.UIs
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Transform suika;
        [SerializeField] private float suikaRotationSpeed;

        private void LateUpdate() => suika.Rotate(Vector3.forward * (suikaRotationSpeed * Time.deltaTime));
    }
}