using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private Image suikaIcon;
        [SerializeField] private Transform suikaTransform;
        [SerializeField] private float suikaRotationSpeed;
        [Inject] private SpriteCreator _spriteCreator;

        public void Init(Sprite sprite) => suikaIcon.sprite = sprite;

        private void Update() => suikaTransform.Rotate(Vector3.forward * (suikaRotationSpeed * Time.deltaTime));

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            Destroy(gameObject);
        }
    }
}