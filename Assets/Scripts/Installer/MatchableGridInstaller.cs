using UnityEngine;
using Zenject;

using TapBlitz.Matchable;
using TapBlitz.Grid;

namespace TapBlitz.Installer
{
    public class MatchableGridInstaller : MonoInstaller
    {
        [SerializeField]
        private TileGrid _tileGrid;

        public override void InstallBindings()
        {
             Container.Bind<IMatchableGrid>().FromInstance(_tileGrid);
            //Container.Bind<IMatchableGrid>().To<TileGrid>().FromComponentInHierarchy().AsCached();
        }
    }
}