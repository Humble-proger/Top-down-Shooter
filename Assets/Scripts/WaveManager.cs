using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour 
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private EnemySpawnManager _enemySpawnManager;
    [SerializeField] private float _timeBetweenWaves = 10f;
    [SerializeField] private Vector2[] _spawnPoints;
    [SerializeField] private float _radiusPlayer;
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _layerMask;

    private int _currentWave = 0;
    private int _enemiesAlive = 0;

    [HideInInspector]
    public static WaveManager Instance { get; private set; }
    [HideInInspector]
    public bool ActiveGenerator = false;
    
    public event Action<int> ChangeEnemyCount;
    public event Action<int> ChangeWave;

    public Difficulty Difficulty { get; set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        StartCoroutine(WaveSpawner());
    }

    private IEnumerator WaveSpawner()
    {
        // Формула для расчета количества врагов
        int baseEnemies, enemyCount;
        while (true)
        {
            yield return new WaitUntil(() => ActiveGenerator);
            _currentWave++;
            Debug.Log($"Increse Wave: {_currentWave}");
            ChangeWave?.Invoke(_currentWave);
            baseEnemies = 1 + (_currentWave * 2);
            enemyCount = Mathf.FloorToInt(baseEnemies * (int)Difficulty);

            IEnumerator enumerator = GenerateEnemiesForWave(enemyCount).GetEnumerator();
            while (enumerator.MoveNext() && ActiveGenerator) {
                if (enumerator.Current is TypeEnemy type) {
                    SpawnEnemy(type);
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 2f));
                }
            }
            yield return new WaitUntil(() => _enemiesAlive == 0);
            if (ActiveGenerator)
                yield return new WaitForSeconds(_timeBetweenWaves);
        }
    }

    private void SpawnEnemy(TypeEnemy enemy)
    {
        Vector2 point = GetSpawnPointAwayPlayer();
        _enemyPool.GetItem(enemy, point);
        _enemiesAlive++;
        ChangeEnemyCount?.Invoke(_enemiesAlive);
    }

    private IEnumerable GenerateEnemiesForWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++) {
            yield return _enemySpawnManager.GetRandomEnemy(_currentWave);
        }
    }

    private Vector2 GetSpawnPointAwayPlayer()
    {
        Vector2 point = GetRandomSpawnPointAwayPlayer();
        if (Vector2.Distance(point, _player.position) >= _radiusPlayer)
            return point;
        return GetSpawnPointAwayPlayer();
    }

    private Vector2 GetRandomSpawnPointAwayPlayer() {
        return _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
    }

    public void OnEnemyDied(TypeEnemy typeEnemy, GameObject enemy) 
    {
        _enemyPool.PutItem(enemy, typeEnemy);
        _enemiesAlive--;
        ChangeEnemyCount?.Invoke(_enemiesAlive);
    }

    public void Reset()
    {
        _currentWave = 0;
        _enemiesAlive = 0;
        ChangeEnemyCount?.Invoke(_enemiesAlive);
        ChangeWave?.Invoke(_currentWave);
        _enemyPool.Reset();
    }
}