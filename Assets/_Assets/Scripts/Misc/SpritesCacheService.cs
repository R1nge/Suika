using _Assets.Scripts.Services.Datas.GameConfigs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Misc
{
    public class SpritesCacheService
    {
        private readonly SpriteCreator _spriteCreator;
        private Sprite[] _suikaSprites;
        private Sprite[] _suikaIconsSprites;

        public async UniTask Preload(GameConfig config, bool isDefault)
        {
            if (!isDefault)
            {
                for (int i = 0; i < config.SuikaSkinsImagesPaths.Length; i++)
                {
                    var path = config.SuikaSkinsImagesPaths[i];
                    _suikaSprites[i] = await SpriteHelper.CreateSprite(path);
                }

                for (int i = 0; i < config.SuikaIconsPaths.Length; i++)
                {
                    var path = config.SuikaIconsPaths[i];
                    _suikaIconsSprites[i] = await SpriteHelper.CreateSprite(path);
                }
            }
            else
            {
                for (int i = 0; i < config.SuikaSkinsImagesPaths.Length; i++)
                {
                    var path = config.SuikaSkinsImagesPaths[i];
                    _suikaSprites[i] = await SpriteHelper.CreateSpriteFromStreamingAssests(path);
                }
                
                
                for (int i = 0; i < config.SuikaIconsPaths.Length; i++)
                {
                    var path = config.SuikaIconsPaths[i];
                    _suikaIconsSprites[i] = await SpriteHelper.CreateSpriteFromStreamingAssests(path);
                }
            }
        }

        public Sprite GetSuikaSprite(int index) => _suikaSprites[index];
        
        public Sprite GetSuikaIconSprite(int index) => _suikaIconsSprites[index];

        public void Reset()
        {
            _suikaSprites = new Sprite[12];
            _suikaIconsSprites = new Sprite[12];
        }
    }
}