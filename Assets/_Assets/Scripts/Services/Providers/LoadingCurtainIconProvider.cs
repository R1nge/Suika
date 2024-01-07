using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.Providers
{
    public class LoadingCurtainIconProvider
    {
        private Sprite _backgroundSprite;
        private Sprite _iconSprite;
        private readonly SpriteCreator _spriteCreator;
        public Sprite BackgroundSprite => _backgroundSprite;
        public Sprite IconSprite => _iconSprite; 
        private LoadingCurtainIconProvider(SpriteCreator spriteCreator)
        {
            _spriteCreator = spriteCreator;
        }

        public async UniTask Load()
        {
            _backgroundSprite = await _spriteCreator.CreateLoadingCurtainBackground();
            _iconSprite = await _spriteCreator.CreateLoadingCurtainIcon();
        }
    }
}