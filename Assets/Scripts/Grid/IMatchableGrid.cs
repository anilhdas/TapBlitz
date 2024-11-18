using System;
using System.Collections;
using UnityEngine;

namespace TapBlitz.Grid
{
    public interface IMatchableGrid
    {
        public Action<int> MatchablesDestroyed { get; set; }
        public IEnumerator CreateGrid(int rowCount, int columnCount, Color[] colors, float tileGenDelay);
    }
}
