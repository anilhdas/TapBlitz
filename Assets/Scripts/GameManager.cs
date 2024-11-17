using TapBlitz.Matchable;
using UnityEngine;

namespace TapBlitz
{
    public class GameManager : MonoBehaviour
    {
        private IMatchableGenerator _generator;

        void Start()
        {
            _generator.GenerateMatchables(0);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}