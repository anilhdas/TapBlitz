using TapBlitz.Util;
using UnityEngine;

namespace TapBlitz.Matchable
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
        }

        public bool IsMatch(Tile otherTile)
        {
            IsVisited = true;
            return _id == otherTile._id;
        }

        public void DestroyTile()
        {
            IsVisited = true;
            _destroyed = true;
        }

        private void ResetTile()
        {
            Assert.IsTrue(!_destroyed, $"Invalid tile {gameObject.name} still not destroyed");
            IsVisited = false;
        }

        #region Unity Callbacks
        void Update()
        {
            if (!_destroyed)
                return;

            // todo: Object pooling
            Destroy(gameObject);
        }
        #endregion
    }
}
