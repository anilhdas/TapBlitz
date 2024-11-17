using Zenject;

using TapBlitz.Grid;

namespace TapBlitz.Installer
{
    public class MatchableGridInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMatchableGrid>().To<TileGrid>().FromComponentInHierarchy().AsCached();
        }
    }
}