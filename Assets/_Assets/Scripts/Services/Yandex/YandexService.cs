using Cysharp.Threading.Tasks;
using YG;

namespace _Assets.Scripts.Services.Yandex
{
    public class YandexService
    {
        public void Init()
        {
            YandexGame.GameReadyAPI();
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