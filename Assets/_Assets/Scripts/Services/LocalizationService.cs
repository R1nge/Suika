using Cysharp.Threading.Tasks;
using UnityEngine.Localization.Settings;

namespace _Assets.Scripts.Services
{
    public class LocalizationService
    {
        public Language CurrentLanguage { get; private set; } = Language.English;

        public async UniTask SetLanguage(Language language)
        {
            await LocalizationSettings.InitializationOperation.Task;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)language];
        }

        public enum Language
        {
            English,
            Russian,
            Turkish
        }
    }
}