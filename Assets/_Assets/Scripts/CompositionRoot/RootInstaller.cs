using _Assets.Scripts.Misc;
using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.Datas.GameConfigs;
using _Assets.Scripts.Services.Datas.Mods;
using _Assets.Scripts.Services.Providers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class RootInstaller : LifetimeScope
    {
        [SerializeField] private ConfigProvider configProvider;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(configProvider);
            builder.Register<ScoreService>(Lifetime.Singleton);
            builder.Register<AudioSettingsDataLoaderJson>(Lifetime.Singleton).As<IAudioSettingsLoader>();
            builder.Register<ModDataLoaderJson>(Lifetime.Singleton).As<IModDataLoader>();
            builder.Register<GameConfigValidator>(Lifetime.Singleton);
            builder.Register<GameConfigLoaderJson>(Lifetime.Singleton).As<IConfigLoader>();
            builder.Register<PlayerDataLoaderJson>(Lifetime.Singleton).As<IPlayerDataLoader>();
            builder.Register<RandomNumberGenerator>(Lifetime.Singleton);
            builder.Register<SpriteCreator>(Lifetime.Singleton);
            builder.Register<SpritesCacheService>(Lifetime.Singleton);
            builder.Register<LoadingCurtainIconProvider>(Lifetime.Singleton);
            builder.Register<MainMenuProvider>(Lifetime.Singleton);
        }
    }
}