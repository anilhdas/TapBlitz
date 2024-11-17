using UnityEngine;

namespace TapBlitz.Grid
{
    public interface IMatchableGrid
    {
        public void CreateGrid(int rowCount, int columnCount, Color[] colors);
    }
}
