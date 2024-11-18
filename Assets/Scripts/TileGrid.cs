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
    public class TileGrid : MonoBehaviour, IMatchableGrid
    {
        private int _rowCount, _columnCount;
        private Color[] _colors;

        private VerticalLayoutGroup[] _columns;
        private Tile[,] _tileGrid;

        #region Unity Callbacks

        void Awake()
        {
        }

        void Update()
        {
        
        }

        #endregion

        public void CreateGrid(int rowCount, int columnCount, Color[] colors)
        {
            var marker = new ProfilerMarker($"{nameof(TileGrid.CreateGrid)}");
            using (marker.Auto())
            {
                _rowCount = rowCount;
                _columnCount = columnCount;

                _colors = colors;

                CreateLayout();

                CreateTiles();
            }
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

        private void CreateTiles()
        {
            _tileGrid = new Tile[_rowCount, _columnCount];

            for (var rowIdx = 0; rowIdx < _rowCount; rowIdx++)
            {
                for (var colIdx = 0; colIdx < _columnCount; colIdx++)
                {
                    var tileId = Random.Range(0, _colors.Length);
                    var tileColor = _colors[tileId];

                    var tileObj = new GameObject($"Tile {rowIdx}, {colIdx}");
                    tileObj.transform.SetParent(_columns[colIdx].transform);

                    tileObj.AddComponent<RectTransform>();
                    tileObj.AddComponent<CanvasRenderer>();

                    var image = tileObj.AddComponent<Image>();
                    image.color = tileColor;
                    
                    var button = tileObj.AddComponent<Button>();
                    
                    // todo: Investigate why we need a copy
                    int rowId = rowIdx;
                    int colId = colIdx;
                    button.onClick.AddListener(() => ButtonPressed(rowId, colId));

                    var tile = tileObj.AddComponent<Tile>();
                    tile.SetId(tileId);
                    tile.SetPosition(rowId, colId);
                    _tileGrid[rowIdx, colIdx] = tile;
                }
            }
        }

        public void ButtonPressed(int rowIdx, int colIdx)
        {
            var destroyedColumns = destroyMatchingTiles(rowIdx, colIdx);

            Debug.Log($"Destroyed {destroyedColumns.Count} tiles");

            regenerateNewTiles(destroyedColumns);

            updateTileGrid();
        }

        private Queue<int> destroyMatchingTiles(int rowIdx, int colIdx)
        {
            var clickedTile = _tileGrid[rowIdx, colIdx];
            Assert.IsTrue(clickedTile != null, $"Invalid Tile at {rowIdx}, {colIdx}");

            var destroyedColumns = new Queue<int>();

            var matchedTiles = new Queue<Tile>();
            matchedTiles.Enqueue(clickedTile);

            // Use Floodfill algorithm to destroy all matching neighbors
            while (matchedTiles.Count > 0)
            {
                var currentTile = matchedTiles.Dequeue();
                currentTile.DestroyTile();
                destroyedColumns.Enqueue(currentTile.ColIdx);

                var neighbors = getValidNeighbors(currentTile.RowIdx, currentTile.ColIdx);

                foreach (var neighbor in neighbors)
                {
                    if (!neighbor.IsVisited && neighbor.IsMatch(currentTile))
                    {
                        matchedTiles.Enqueue(neighbor);
                    }
                }
            }

            return destroyedColumns;
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
                // _columns[colIdx].AddTile();
            }
        }

        private void updateTileGrid()
        {
            for (int colIdx = 0; colIdx < _columnCount; colIdx++)
            {
                var tiles = _columns[colIdx].transform.GetComponentsInChildren<Tile>();
                var rowCount = tiles.Length;
                //Assert.IsTrue(_rowCount == tiles.Length, "Tile count should be the same as the config");
                for (int rowIdx = 0; rowIdx < rowCount; rowIdx++)
                {
                    var tile = tiles[rowIdx];
                    tile.SetPosition(rowIdx, colIdx);
                    _tileGrid[rowIdx, colIdx] = tile;
                }
            }
        }
    }
}