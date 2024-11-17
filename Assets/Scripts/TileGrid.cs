using UnityEngine;
using UnityEngine.UI;

using TapBlitz.Matchable;
using TapBlitz.Util;

namespace TapBlitz.Grid
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class TileGrid : MonoBehaviour
    {
        private VerticalLayoutGroup[] _columns;

        private Color[] _colors;
        private Tile[] _tileGrid;

        #region Unity Callbacks

        void Awake()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        #endregion


        private void CreateGrid(int rowCount, int columnCount, Color[] colors)
        {
            CreateLayout(columnCount);

            _colors = colors;

            int tileCount = rowCount * columnCount;

            _tileGrid = new Tile[tileCount];

            for (var i = 0; i < tileCount; i++)
            {
                int tileId = Random.Range(0, _colors.Length);

            }
        }

        private void CreateLayout(int columns)
        {
            var layoutGroup = gameObject.GetComponent<HorizontalLayoutGroup>();

            layoutGroup.spacing = 20f;
            layoutGroup.childAlignment = TextAnchor.MiddleCenter;

            layoutGroup.reverseArrangement = false;

            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;

            layoutGroup.childForceExpandWidth = true;
            layoutGroup.childForceExpandHeight = true;

            _columns = new VerticalLayoutGroup[columns];

            for (int i = 0; i < columns; i++)
            {
                var column = new GameObject($"Column {i}");

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

        }

        public void ButtonPressed(int buttonId)
        {

        }
    }
}