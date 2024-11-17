using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
public class GameConfig : ScriptableObject
{
    public LevelConfig[] levelConfigs;
}
