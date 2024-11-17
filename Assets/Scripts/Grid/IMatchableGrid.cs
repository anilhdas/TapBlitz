using UnityEngine;

namespace TapBlitz.Grid
{
    public interface IMatchableGrid
    {
        public string MyName { get; set; }
        public void CreateGrid(int rowCount, int columnCount, Color[] colors);
    }
}
