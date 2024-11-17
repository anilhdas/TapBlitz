using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Header("N")]
    public int TotalRows = 5;

    [Header("M")]
    public int TotalCols = 4;

    [Header("P")]
    public int TotalColors = 4;
}
