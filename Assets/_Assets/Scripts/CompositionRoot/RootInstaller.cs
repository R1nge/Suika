﻿using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.Datas.GameConfigs;
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
            builder.Register<GameConfigLoaderJson>(Lifetime.Singleton).As<IConfigLoader>();
            builder.Register<PlayerDataLoaderPlayerPrefs>(Lifetime.Singleton).As<IPlayerDataLoader>();
        }
    }
}