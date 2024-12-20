using System.ComponentModel;
using UnityEngine;

namespace TapBlitz.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public LevelConfig[] levelConfigs;

        public float TileGenDelay = 0.005f;
    }
}
