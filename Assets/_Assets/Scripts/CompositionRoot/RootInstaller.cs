using _Assets.Scripts.Services;
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
        }
    }
}