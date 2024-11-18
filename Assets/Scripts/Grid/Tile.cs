using UnityEngine;
using TapBlitzUtils;

namespace TapBlitz.Grid
{
    public class Tile : MonoBehaviour
    {
        private int _id = -1;
        private bool _destroyed = false;

        public int RowIdx { get; private set; }
        public int ColIdx { get; private set; }

        public bool IsVisited { get; private set; }

        public void SetId(int newId)
        {
            Assert.IsTrue(_id == -1, $"Tile {gameObject.name} already has an id set");
            Assert.IsTrue(newId >= 0, $"Invalid tile id being set on Tile {gameObject.name}");

            _id = newId;
        }

        public void SetPosition(int row, int col)
        {
            RowIdx = row;
            ColIdx = col;

            ResetTile();
        }

        public void MarkVisited()
        {
            IsVisited = true;
        }

        public bool IsMatch(Tile otherTile)
        {
            MarkVisited();
            return _id == otherTile._id;
        }

        private void ResetTile()
        {
            Assert.IsTrue(!_destroyed, $"Invalid tile {gameObject.name} still not destroyed");
            IsVisited = false;
        }
    }
}
