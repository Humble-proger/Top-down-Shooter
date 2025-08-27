using UnityEngine;

public interface IEnemy {
    void TakeDamage(float damage);
    void Reset();
    void Alert(Vector2 point);
}
