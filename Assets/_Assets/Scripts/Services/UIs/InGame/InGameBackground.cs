using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class InGameBackground : MonoBehaviour
    {
        [SerializeField] private Image inGameBackground;
        [Inject] private SpriteCreator _spriteCreator;

        public async UniTask Init()
        {
            inGameBackground.sprite = await _spriteCreator.CreateInGameBackground();
            Color newColor = inGameBackground.color;
            newColor.a = 255;
            inGameBackground.color = newColor;
        }
    }
}