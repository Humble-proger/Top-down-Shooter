using UnityEngine;

public class Point : MonoBehaviour
{
    public Vector2 point;
    private void OnValidate()
    {
        point = transform.position;
    }
}
