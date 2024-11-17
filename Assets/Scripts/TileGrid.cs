using UnityEngine;
using UnityEngine.UI;

using TapBlitz.Matchable;
using TapBlitz.Util;

namespace TapBlitz.Grid
{
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

        // Update is called once per frame
        void Update()
        {
        
        }

        #endregion


        public void CreateGrid(int rowCount, int columnCount, Color[] colors)
        {
            _rowCount = rowCount;
            _columnCount = columnCount;

            _colors = colors;

            CreateLayout();

            CreateTiles();
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
                    int tileId = Random.Range(0, _colors.Length);

                    //_tileGrid[rowIdx][colIdx] = 
                }
            }
        }

        public void ButtonPressed(int buttonId)
        {

        }
    }
}