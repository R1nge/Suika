using _Assets.Scripts.Misc;
using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs;
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
        [SerializeField] private ContainerFactory containerFactory;
        [SerializeField] private ModSlotFactory modSlotFactory;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerInput>(Lifetime.Singleton);
            
            builder.RegisterComponent(coroutineRunner);
            builder.RegisterComponent(playerFactory);
            builder.RegisterComponent(containerFactory);
            builder.RegisterComponent(modSlotFactory);


            builder.Register<RandomNumberGenerator>(Lifetime.Singleton);

            builder.Register<SuikaUIDataProvider>(Lifetime.Singleton);
            builder.Register<SuikasFactory>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameOverTimer>().AsSelf();
            builder.Register<ResetService>(Lifetime.Singleton);

            builder.Register<LeaderBoardService>(Lifetime.Singleton);
            
            builder.Register<UIStatesFactory>(Lifetime.Singleton);
            builder.Register<UIStateMachine>(Lifetime.Singleton);
            
            builder.Register<GameStatesFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameOverService>();
        }
    }
}