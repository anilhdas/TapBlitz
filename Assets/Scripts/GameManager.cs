using UnityEngine;
using Zenject;

using TapBlitz.Config;
using TapBlitz.Grid;
using TapBlitzUtils;

namespace TapBlitz
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameConfig _gameConfig;

        [Inject]
        private IMatchableGrid _grid;

        private int _currentLevel;
        private int _score;
        private int _turns;

        #region Unity callbacks

        private void Awake()
        {
            Assert.IsTrue(_gameConfig != null, $"{nameof(_gameConfig)} in gameobject {gameObject.name} is not assigned");
            Assert.IsTrue(_grid != null, $"{nameof(_grid)} in gameobject {gameObject.name} is not injected");

            resetGame();

            _grid.MatchablesDestroyed += HandleMatchableDestroyed;
        }

        void Start()
        {
            loadNextLevel();
        }

        #endregion

        private void HandleMatchableDestroyed(int count)
        {
            _turns++;
            _score += count;

            Debug.Log($"Turns: {_turns} || Score: {_score}");
        }

        private void loadNextLevel()
        {
            _currentLevel++;

            var levelConfig = _gameConfig.levelConfigs[_currentLevel];

            StartCoroutine(_grid.CreateGrid(levelConfig.TotalRows, levelConfig.TotalCols, levelConfig.TileColors, _gameConfig.TileGenDelay));
        }

        private void resetGame()
        {
            _currentLevel = -1;
            _score = 0;
            _turns = 0;
        }
    }
}