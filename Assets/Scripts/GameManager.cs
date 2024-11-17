using TapBlitz.Util;
using TapBlitz.Matchable;

using Zenject;
using UnityEngine;
using TapBlitz.Config;
using TapBlitz.Grid;

namespace TapBlitz
{
    public class GameManager : MonoBehaviour
    {
        [Inject]
        private IMatchableGrid _grid;

        [SerializeField]
        private GameConfig _gameConfig;

        private int _currentLevel;

        #region Unity callbacks

        private void Awake()
        {
            Assert.IsTrue(_grid != null, $"{nameof(_grid)} in gameobject {gameObject.name} is not injected");
            Assert.IsTrue(_gameConfig != null, $"{nameof(_gameConfig)} in gameobject {gameObject.name} is not assigned");

            resetGame();
        }

        void Start()
        {
            loadNextLevel();
        }

        #endregion

        private void loadNextLevel()
        {
            UnityEngine.Profiling.Profiler.BeginSample($"{nameof(loadNextLevel)}");
            _currentLevel++;

            var levelConfig = _gameConfig.levelConfigs[_currentLevel];

            //_grid.CreateGrid(levelConfig.TotalRows, levelConfig.TotalCols, levelConfig.TileColors);

            //var initialMatchables = _generator.GenerateMatchables(totalMatchables, totalColors.Length);
        }

        private void resetGame()
        {
            _currentLevel = -1;
        }
    }

}