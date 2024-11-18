using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Profiling;

using TapBlitz.Matchable;
using TapBlitz.Util;

namespace TapBlitz.Grid
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasRenderer))]
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    [RequireComponent(typeof(CanvasGroup))]
    public class TileGrid : MonoBehaviour, IMatchableGrid
    {
        private CanvasGroup _canvasGroup;

        private int _rowCount, _columnCount;
        private Color[] _colors;
        private float _tileGenDelay;

        private VerticalLayoutGroup[] _columns;
        private Tile[,] _tileGrid;

        #region Unity Callbacks

        void Awake()
        {
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        #endregion

        public IEnumerator CreateGrid(int rowCount, int columnCount, Color[] colors, float tileGenDelay)
        {
            setGridInteractable(false);

            _rowCount = rowCount;
            _columnCount = columnCount;
            _colors = colors;
            _tileGenDelay = tileGenDelay;

            CreateLayout();

            yield return StartCoroutine(CreateTiles());

            updateTileGrid();

            setGridInteractable(true);
        }

        private void CreateLayout()
        {
            var layoutGroup = gameObject.GetComponent<HorizontalLayoutGroup>();

            layoutGroup.spacing = 20f;
            layoutGroup.childAlignment = TextAnchor.MiddleCenter;

            layoutGroup.reverseArrangement = true;

            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;

            layoutGroup.childForceExpandWidth = true;
            layoutGroup.childForceExpandHeight = true;

            _columns = new VerticalLayoutGroup[_columnCount];

            for (int i = 0; i < _columnCount; i++)
            {
                var column = new GameObject($"Column {i}");
                column.transform.SetParent(layoutGroup.transform);

                column.AddComponent<RectTransform>();
                column.AddComponent<CanvasRenderer>();

                _columns[i] = column.AddComponent<VerticalLayoutGroup>();

                _columns[i].spacing = 20f;
                _columns[i].childAlignment = TextAnchor.LowerCenter;

                _columns[i].reverseArrangement = true;

                _columns[i].childControlWidth = false;
                _columns[i].childControlHeight = false;

                _columns[i].childForceExpandWidth = true;
                _columns[i].childForceExpandHeight = true;
            }

        }

        private IEnumerator CreateTiles()
        {
            _tileGrid = new Tile[_rowCount, _columnCount];

            for (var rowIdx = 0; rowIdx < _rowCount; rowIdx++)
            {
                for (var colIdx = 0; colIdx < _columnCount; colIdx++)
                {
                    yield return new WaitForSeconds(_tileGenDelay);
                    CreateRandomTile(colIdx);
                }
            }
        }

        private void CreateRandomTile(int colIdx)
        {
            var tileId = Random.Range(0, _colors.Length);
            var tileColor = _colors[tileId];

            var tileObj = new GameObject("Tile");
            tileObj.transform.SetParent(_columns[colIdx].transform);

            tileObj.AddComponent<RectTransform>();
            tileObj.AddComponent<CanvasRenderer>();
            tileObj.AddComponent<Image>().color = tileColor;
            tileObj.AddComponent<Button>();
            tileObj.AddComponent<Tile>().SetId(tileId);
        }

        public void ButtonPressed(int rowIdx, int colIdx)
        {
            setGridInteractable(false);

            Queue<int> affectedColumns = new Queue<int>();
            StartCoroutine(destroyMatchingTiles(rowIdx, colIdx, affectedColumns));

            Debug.Log($"Destroyed {affectedColumns.Count} tiles");

            regenerateNewTiles(affectedColumns);

            updateTileGrid();

            setGridInteractable(true);
        }

        private void setGridInteractable(bool interactable)
        {
            _canvasGroup.interactable = interactable;
        }

        private IEnumerator destroyMatchingTiles(int rowIdx, int colIdx, Queue<int> affectedColumns)
        {
            var clickedTile = _tileGrid[rowIdx, colIdx];
            Assert.IsTrue(clickedTile != null, $"Invalid Tile at {rowIdx}, {colIdx}");

            Assert.IsTrue(affectedColumns != null && affectedColumns.Count == 0, $"{affectedColumns} is either null or not empty");

            var matchedTiles = new Queue<Tile>();
            matchedTiles.Enqueue(clickedTile);

            // Use Floodfill algorithm to destroy all matching neighbors
            while (matchedTiles.Count > 0)
            {
                var currentTile = matchedTiles.Dequeue();
                affectedColumns.Enqueue(currentTile.ColIdx);
                currentTile.MarkVisited();
                yield return new WaitForSeconds(_tileGenDelay);

                var neighbors = getValidNeighbors(currentTile.RowIdx, currentTile.ColIdx);

                foreach (var neighbor in neighbors)
                {
                    if (!neighbor.IsVisited && neighbor.IsMatch(currentTile))
                    {
                        matchedTiles.Enqueue(neighbor);
                    }
                }

                Destroy(currentTile.gameObject);
            }
        }

        private List<Tile> getValidNeighbors(int rowIdx, int colIdx)
        {
            var currentTile = _tileGrid[rowIdx, colIdx];

            var matches = new List<Tile>();

            if (rowIdx + 1 < _rowCount)
            {
                matches.Add(_tileGrid[rowIdx + 1, colIdx]);
            }

            if (rowIdx - 1 >= 0)
            {
                matches.Add(_tileGrid[rowIdx - 1, colIdx]);
            }

            if (colIdx + 1 < _columnCount)
            {
                matches.Add(_tileGrid[rowIdx, colIdx + 1]);
            }

            if (colIdx - 1 >= 0)
            {
                matches.Add(_tileGrid[rowIdx, colIdx - 1]);
            }

            return matches;
        }

        private void regenerateNewTiles(Queue<int> destroyedColumns)
        {
            while (destroyedColumns.Count > 0)
            {
                var colIdx = destroyedColumns.Dequeue();
                CreateRandomTile(colIdx);
            }
        }

        private void updateTileGrid()
        {
            var marker = new ProfilerMarker($"{nameof(updateTileGrid)}");
            using (marker.Auto())
            {
                for (int colIdx = 0; colIdx < _columnCount; colIdx++)
                {
                    var tiles = _columns[colIdx].transform.GetComponentsInChildren<Tile>();
                    Assert.IsTrue(tiles.Length == _rowCount, $"Current tile count {tiles.Length} differs from config {_rowCount} in column {colIdx}");

                    for (int rowIdx = 0; rowIdx < _rowCount; rowIdx++)
                    {
                        var tile = tiles[rowIdx];
                    
                        var button = tile.transform.GetComponent<Button>();
                        Assert.IsTrue(button != null, $"Unable to find button in tile {rowIdx}, {colIdx}");
                        button.onClick.RemoveAllListeners();

                        // todo: Investigate why we need copies
                        int rowId = rowIdx;
                        int colId = colIdx;
                        button.onClick.AddListener(() => {ButtonPressed(rowId, colId); });

                        tile.SetPosition(rowIdx, colIdx);
                        _tileGrid[rowIdx, colIdx] = tile;
                    }
                }
            }
        }
    }
}