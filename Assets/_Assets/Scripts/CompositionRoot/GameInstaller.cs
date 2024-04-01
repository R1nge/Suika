using _Assets.Scripts.Misc;
using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.GameModes;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs;
using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Vibrations;
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
        [SerializeField] private AudioService audioService;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private GamepadRumbleService gamepadRumbleService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(coroutineRunner);
            builder.RegisterComponent(playerFactory);
            builder.RegisterComponent(containerFactory);
            builder.RegisterComponent(modSlotFactory);
            builder.RegisterComponent(audioService);
            builder.RegisterComponent(playerInput);

            builder.Register<TimeRushTimer>(Lifetime.Singleton).As<ITickable>().AsSelf();
            builder.Register<GameModeService>(Lifetime.Singleton);
            
            builder.RegisterComponent(gamepadRumbleService);
            builder.Register<VibrationSettingsDataLoaderJson>(Lifetime.Singleton).As<IVibrationSettingLoader>();
            builder.Register<VibrationService>(Lifetime.Singleton);

            builder.Register<CollisionService>(Lifetime.Singleton);
            
            builder.Register<SuikaUIDataProvider>(Lifetime.Singleton);
            builder.Register<SuikasFactory>(Lifetime.Singleton);


            builder.Register<ContinueGameService>(Lifetime.Singleton);
            
            
            builder.RegisterEntryPoint<GameOverTimer>().AsSelf();
            builder.Register<ResetService>(Lifetime.Singleton);

            builder.Register<LeaderBoardService>(Lifetime.Singleton);

            builder.Register<UIFactory>(Lifetime.Singleton);
            builder.Register<UIStatesFactory>(Lifetime.Singleton);
            builder.Register<UIStateMachine>(Lifetime.Singleton);
            
            builder.Register<GameStatesFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameOverService>();
            builder.RegisterEntryPoint<TimerRushGameOver>();
        }
    }
}