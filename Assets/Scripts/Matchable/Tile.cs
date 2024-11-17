namespace TapBlitz.Matchable
{
    public class Tile : IMatchable
    {
        private readonly int _id;

        public int Id => _id;

        public Tile(int tileId)
        {
            _id = tileId;
        }
    }
}
