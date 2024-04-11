using _Assets.Scripts.Services.Yandex;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace _Assets.Scripts.Services
{
    public class LocalizationService
    {
        private readonly YandexService _yandexService;
        private bool _loaded;

        private LocalizationService(YandexService yandexService)
        {
            _yandexService = yandexService;
        }

        public async UniTask SetLanguage(Language language)
        {
            await LocalizationSettings.InitializationOperation.Task;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)language];
        }

        public async UniTask InitYandex(string str)
        {
            LocalizationSettings.SelectedLocaleChanged += Loaded;
            await LocalizationSettings.InitializationOperation.Task;

            if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
            {
                _loaded = true;
            }
            
            if (str == "en")
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
            }
            else if (str == "ru")
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
            }
            else if (str == "tr")
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
            }
            else
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
            }
           
            await UniTask.WaitUntil(() => _loaded);
        }

        private void Loaded(Locale locale)
        {
            _loaded = true;
            LocalizationSettings.SelectedLocaleChanged -= Loaded;
        }

        public enum Language
        {
            English = 0,
            Russian = 1,
            Turkish = 2
        }
    }
}