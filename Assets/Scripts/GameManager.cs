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
        [SerializeField]
        private GameConfig _gameConfig;

        [Inject]
        private IMatchableGrid _grid;

        private int _currentLevel;

        #region Unity callbacks

        private void Awake()
        {
            Assert.IsTrue(_gameConfig != null, $"{nameof(_gameConfig)} in gameobject {gameObject.name} is not assigned");
            Assert.IsTrue(_grid != null, $"{nameof(_grid)} in gameobject {gameObject.name} is not injected");

            resetGame();
        }

        void Start()
        {
            loadNextLevel();
        }

        #endregion

        private void loadNextLevel()
        {
            _currentLevel++;

            var levelConfig = _gameConfig.levelConfigs[_currentLevel];

            StartCoroutine(_grid.CreateGrid(levelConfig.TotalRows, levelConfig.TotalCols, levelConfig.TileColors, _gameConfig.TileGenDelay));
        }

        private void resetGame()
        {
            _currentLevel = -1;
        }
    }

}