using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawnData> _enemySpawnData = new();
    
    private int _totalWeight = 0;

    private void Awake()
    {
        CalculateWeights();
    }

    private void CalculateWeights()
    {
        _totalWeight = 0;

        foreach (var enemyData in _enemySpawnData)
        {
            _totalWeight += enemyData.Weight;
            enemyData.CumulativeWeight = _totalWeight;
        }
    }

    public TypeEnemy GetRandomEnemy(int currentLevel = 1)
    {
        if (_enemySpawnData.Count == 0) throw new IndexOutOfRangeException("Enemies were not specified");

        int randomValue = UnityEngine.Random.Range(0, _totalWeight);

        foreach (var enemyData in _enemySpawnData)
        {
            // ѕровер€ем уровень доступности
            if (currentLevel >= enemyData.MinLevel && currentLevel <= enemyData.MaxLevel)
            {
                if (randomValue < enemyData.CumulativeWeight)
                {
                    return enemyData.TypeEnemy;
                }
            }
        }

        // ≈сли ни один не подошел по уровню, возвращаем любого
        return GetAnyEnemy();
    }

    private TypeEnemy GetAnyEnemy()
    {
        int randomIndex = UnityEngine.Random.Range(0, _enemySpawnData.Count);
        return _enemySpawnData[randomIndex].TypeEnemy;
    }

    // ћетод дл€ динамического изменени€ весов
    public void AdjustEnemyWeight(TypeEnemy typeEnemy, int newWeight)
    {
        var enemyData = _enemySpawnData.Find(data => data.TypeEnemy == typeEnemy);
        if (enemyData != null)
        {
            enemyData.Weight = newWeight;
            CalculateWeights(); // ѕересчитываем общий вес
        }
    }
}