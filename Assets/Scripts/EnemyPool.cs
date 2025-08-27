using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPool : MonoBehaviour 
{
    public TypeEnemy[] _enemyTypes;
    public GameObject[] _enemyPrefabs;
    public Transform[] _enemyConteiners;

    private Dictionary<TypeEnemy, Queue<GameObject>> _enemyPools;

    private void Awake()
    {
        _enemyPools = new();
        foreach (var val in _enemyTypes) {
            _enemyPools[val] = new Queue<GameObject>();
        }
    }

    public GameObject GetItem(TypeEnemy type, Vector2 position)
    {
        GameObject enemy;
        int index = Array.IndexOf(_enemyTypes, type);
        if (_enemyPools[type].Count == 0)
        {
            enemy = Instantiate(_enemyPrefabs[index], position, Quaternion.identity);
        }
        else
        {
            enemy = _enemyPools[type].Dequeue();
            var enemyObj = enemy.GetComponent<IEnemy>();
            enemyObj.Reset();
            enemy.SetActive(true);
        }
        enemy.transform.position = (Vector3) position;
        enemy.transform.parent = _enemyConteiners[index];
        return enemy;
    }

    public void PutItem(GameObject obj, TypeEnemy type)
    {
        _enemyPools[type].Enqueue(obj);
        obj.SetActive(false);
    }

    public void Reset()
    {
        foreach (var val in _enemyTypes)
            _enemyPools[val].Clear();

        for (int index = 0; index < _enemyTypes.Length; index++) {
            foreach (Transform child in _enemyConteiners[index])
            {
                if (child.TryGetComponent(out IEnemy enemy))
                {
                    PutItem(child.gameObject, _enemyTypes[index]);
                }
            }
        }
    }
}