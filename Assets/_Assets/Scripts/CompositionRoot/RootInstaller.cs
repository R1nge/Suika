﻿using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Configs;
using _Assets.Scripts.Services.Datas;
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
            builder.Register<DataServicePlayerPrefs>(Lifetime.Singleton).As<IDataService>();
        }
    }
}