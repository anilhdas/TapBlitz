using UnityEngine;

namespace TapBlitz.Config
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public int TotalRows = 5;
        public int TotalCols = 4;
        public int TotalColors = 4;
    }
}
