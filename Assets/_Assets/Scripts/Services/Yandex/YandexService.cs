using System;
using Cysharp.Threading.Tasks;
using YG;

namespace _Assets.Scripts.Services.Yandex
{
    public class YandexService
    {
        public event Action OnFullScreenAdShown;
        public event Action OnFullScreenAdClosed;

        public async UniTask Init()
        {
            YandexGame.GameReadyAPI();
            await UniTask.WaitUntil(() => YandexGame.SDKEnabled);
            YandexGame.OpenFullAdEvent += OnFullScreenAdShown;
            YandexGame.CloseFullAdEvent += OnFullScreenAdClosed;
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