using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private CoroutineRunner coroutineRunner;
        [SerializeField] private PlayerFactory playerFactory;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(coroutineRunner);
            builder.RegisterComponent(playerFactory);
            
            //TODO: leader board service
            
            builder.Register<UIStatesFactory>(Lifetime.Singleton);
            builder.Register<UIStateMachine>(Lifetime.Singleton);
            
            builder.Register<GameStatesFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);
        }
    }
}