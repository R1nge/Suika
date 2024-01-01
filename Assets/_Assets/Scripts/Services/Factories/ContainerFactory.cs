using System.IO;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Factories
{
    public class ContainerFactory : MonoBehaviour
    {
        [SerializeField] private GameObject containerPrefab;
        [SerializeField] private Transform spawnPoint;
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private ResetService _resetService;
        [Inject] private IConfigLoader _configLoader;

        public void Create()
        {
            var container = _objectResolver.Instantiate(containerPrefab, spawnPoint.position, Quaternion.identity);
            var imagePath = _configLoader.CurrentConfig.ContainerImagePath;
            var imageBytes = File.ReadAllBytes(imagePath);
            var imageTexture = new Texture2D(128, 128);
            imageTexture.LoadImage(imageBytes);
            var imageSprite = Sprite.Create(imageTexture, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));
            container.GetComponentInChildren<SpriteRenderer>().sprite = imageSprite;
            _resetService.SetContainer(container);
        }
    }
}