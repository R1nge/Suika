using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.Providers
{
    public class MainMenuProvider
    {
        private Sprite _backgroundSprite;
        private readonly SpriteCreator _spriteCreator;
        
        public Sprite BackgroundSprite => _backgroundSprite;

        public MainMenuProvider(SpriteCreator spriteCreator) => _spriteCreator = spriteCreator;

        public async UniTask Load() => _backgroundSprite = await _spriteCreator.CreateMainMenuBackground();
    }
}