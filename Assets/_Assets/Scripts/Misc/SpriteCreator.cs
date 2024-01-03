using _Assets.Scripts.Services.Datas.GameConfigs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Misc
{
    public class SpriteCreator
    {
        private readonly IConfigLoader _configLoader;

        public SpriteCreator(IConfigLoader configLoader) => _configLoader = configLoader;

        public async UniTask<Sprite> CreateSuikaIconSprite(int index)
        {
            if (!_configLoader.IsDefault)
            {
                var path = _configLoader.CurrentConfig.SuikaIconsPaths[index];
                var size = StaticData.SuikaIconSpriteSize;
                return await SpriteHelper.CreateSprite(path, size, size);
            }
            else
            {
                var path = _configLoader.CurrentConfig.SuikaIconsPaths[index];
                var sprite = await SpriteHelper.CreateSpriteFromStreamingAssests(path, StaticData.SuikaIconSpriteSize,
                    StaticData.SuikaIconSpriteSize);
                return sprite;
            }
        }

        public async UniTask<Sprite> CreateSuikaSprite(int index)
        {
            if (!_configLoader.IsDefault)
            {
                var path = _configLoader.CurrentConfig.SuikaSkinsImagesPaths[index];
                var size = StaticData.SuikaSkinSpriteSize;
                return await SpriteHelper.CreateSprite(path, size, size);
            }
            else
            {
                var path = _configLoader.CurrentConfig.SuikaSkinsImagesPaths[index];
                var size = StaticData.SuikaSkinSpriteSize;
                return await SpriteHelper.CreateSpriteFromStreamingAssests(path, size, size);
            }
        }

        public async UniTask<Sprite> CreateContainerSprite()
        {
            var path = _configLoader.CurrentConfig.ContainerImagePath;
            var size = StaticData.ContainerSpriteSize;
            return await SpriteHelper.CreateSprite(path, size, size);
        }
    }
}