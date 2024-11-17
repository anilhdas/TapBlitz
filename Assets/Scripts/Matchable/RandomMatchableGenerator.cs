using UnityEngine;
using TapBlitz.Util;

namespace TapBlitz.Matchable
{
    public class RandomMatchableGenerator : IMatchableGenerator
    {
        public IMatchable[] GenerateMatchables(int count, int types)
        {
            Assert.IsTrue(count > 0, "Invalid count to generate matchables");

            IMatchable[] matchables = new IMatchable[count];

            for(var i = 0; i < count; i++)
            {
                int tileId = Random.Range(0, types);
                matchables[i] = new Tile(tileId);
            }

            return matchables;
        }

    }
}
