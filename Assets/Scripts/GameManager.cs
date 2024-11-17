using TapBlitz.Util;
using TapBlitz.Matchable;

using Zenject;
using UnityEngine;
using TapBlitz.Config;

namespace TapBlitz
{
    public class GameManager : MonoBehaviour
    {
        [Inject]
        private IMatchableGenerator _generator;

        [SerializeField]
        private GameConfig _gameConfig;

        private int _currentLevel;

        #region Unity callbacks

        private void Awake()
        {
            Assert.IsTrue(_generator != null, $"{nameof(_generator)} in gameobject {gameObject.name} is not assigned");
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

            var totalMatchables = levelConfig.TotalRows * levelConfig.TotalCols;
            var totalColors = levelConfig.TotalColors;

            var initialMatchables = _generator.GenerateMatchables(totalMatchables, totalColors);
        }

        private void resetGame()
        {
            _currentLevel = -1;
        }
    }

}