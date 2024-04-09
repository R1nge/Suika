using Cysharp.Threading.Tasks;
using YG;

namespace _Assets.Scripts.Services.Yandex
{
    public class YandexService
    {
        public async UniTask Init()
        {
            YandexGame.GameReadyAPI();
            await UniTask.WaitUntil(() => YandexGame.SDKEnabled);
        }

        public void ShowStickyAd()
        {
       
        }
        
        public void HideStickyAd()
        {
        
        }

        public void ShowVideoAd()
        {
            YandexGame.FullscreenShow();
        }

        public void ShowInterstitialAd()
        {
        }
    }
}