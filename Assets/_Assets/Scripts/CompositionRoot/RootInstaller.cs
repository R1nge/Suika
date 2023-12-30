﻿using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class RootInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ScoreService>(Lifetime.Singleton);
            builder.Register<DataServicePlayerPrefs>(Lifetime.Singleton).As<IDataService>();
            //TODO: leader board service

            builder.Register<UIStatesFactory>(Lifetime.Singleton);
            builder.Register<UIStateMachine>(Lifetime.Singleton);

            builder.Register<GameStatesFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);
        }
    }
}