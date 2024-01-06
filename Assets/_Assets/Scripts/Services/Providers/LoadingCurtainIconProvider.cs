using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.Providers
{
    public class LoadingCurtainIconProvider
    {
        private Sprite _sprite;
        private readonly SpriteCreator _spriteCreator;
        public Sprite Sprite => _sprite; 
        private LoadingCurtainIconProvider(SpriteCreator spriteCreator)
        {
            _spriteCreator = spriteCreator;
        }

        public async UniTask Load()
        {
            _sprite = await _spriteCreator.CreateLoadingCurtainIcon();
        }
    }
}