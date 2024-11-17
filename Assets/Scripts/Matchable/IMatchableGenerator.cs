using UnityEngine;

namespace TapBlitz.Matchable
{
    public interface IMatchableGenerator
    {
        public IMatchable[] GenerateMatchables(int count, int colors);
    }
}