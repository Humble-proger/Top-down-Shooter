using UnityEngine;

[CreateAssetMenu(fileName = "New enemy", menuName = "Enemys/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public float SpeedChaise;
    public float SpeedPatrol;
    public float SpeedAlert;
    public float VisibilityRange;
    public float Damage;
    public float Health;
    public float AttackRange;
    public float AttackCooldown;
    public float StoppingDistance = 0.5f;
    public Vector2[] PatrolPoints;
}
