using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public TypeEnemy TypeEnemy;
    public int Weight = 1;
    public int MinLevel = 1;
    public int MaxLevel = 99;
    [HideInInspector] public int CumulativeWeight;
}
