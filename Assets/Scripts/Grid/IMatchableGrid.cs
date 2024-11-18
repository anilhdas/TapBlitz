using System.Collections;
using UnityEngine;

namespace TapBlitz.Grid
{
    public interface IMatchableGrid
    {
        public IEnumerator CreateGrid(int rowCount, int columnCount, Color[] colors, float tileGenDelay);
    }
}
