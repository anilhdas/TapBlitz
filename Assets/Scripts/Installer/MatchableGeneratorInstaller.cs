using Zenject;
using TapBlitz.Matchable;

namespace TapBlitz.Installer
{
    public class MatchableGeneratorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMatchableGenerator>().To<RandomMatchableGenerator>().AsSingle();
        }
    }
}