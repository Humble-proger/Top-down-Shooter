using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour 
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _container;

    private Queue<Bullet> _bulletPool;

    public static BulletPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _bulletPool = new Queue<Bullet>();
    }

    public Bullet GetItem()
    {
        Bullet bullet;
        if (_bulletPool.Count == 0)
        {
            bullet = Instantiate(_bulletPrefab);
        }
        else
        {
            bullet = _bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            bullet.Reset();
        }
        bullet.transform.parent = _container;
        return bullet;
    }

    public void PutItem(Bullet obj)
    {
        _bulletPool.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _bulletPool.Clear();
        foreach (Transform child in _container)
        {
            if (child.TryGetComponent(out Bullet bullet))
            {
                PutItem(bullet);
            }
        }
    }
}