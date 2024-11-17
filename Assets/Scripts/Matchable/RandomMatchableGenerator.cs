using UnityEngine;
using TapBlitz.Util;

namespace TapBlitz.Matchable
{
    public class RandomMatchableGenerator : IMatchableGenerator
    {
        public IMatchable[] GenerateMatchables(int count)
        {
            Assert.IsTrue(count > 0, "Invalid count to generate matchables");

            IMatchable[] matchables = new Tile[count];

            // Generate random Tile Ids

            return matchables;
        }
    }
}
