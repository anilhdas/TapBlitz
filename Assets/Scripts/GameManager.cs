using TapBlitz.Util;
using TapBlitz.Matchable;
using UnityEngine;

namespace TapBlitz
{
    public class GameManager : MonoBehaviour
    {
        private IMatchableGenerator _generator;

        void Start()
        {
            Assert.IsTrue(_generator != null, $"{nameof(_generator)} in gameobject {gameObject.name} is not assigned");

            _generator.GenerateMatchables(0);
        }

        void Update()
        {
        
        }
    }

}