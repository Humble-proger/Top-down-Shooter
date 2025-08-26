using UnityEngine;

public class Bullet : MonoBehaviour 
{
    [SerializeField] private float _velocityBullet;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime;

    [HideInInspector] public float CurrentLifeTime = 0f;

    private void FixedUpdate()
    {
        if (CurrentLifeTime < _lifeTime) {
            transform.Translate(_velocityBullet * Time.fixedDeltaTime * Vector2.up);
            CurrentLifeTime += Time.fixedDeltaTime;
            return;
        }
        BulletPool.Instance.PutItem(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletPool.Instance.PutItem(this);
        if (collision.TryGetComponent(out IEnemy enemy))
        {
            enemy.TakeDamage(_damage);
            Debug.Log("Damage");
        }
    }

    public void Reset()
    {
        CurrentLifeTime = 0f;
    }
}