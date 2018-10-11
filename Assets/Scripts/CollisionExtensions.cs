using UnityEngine;

public static class CollisionExtensions
{
    public static bool CheckCollision(this Collision2D col, GameObject collidingGameObject)
    {
        return col.gameObject == collidingGameObject;
    }

    public static bool CheckCollision(this Collider2D col, GameObject collidingGameObject)
    {
        return col.gameObject == collidingGameObject;
    }

    public static bool TryGetComponent<T>(this Collider2D collider, out T component) where T : class
    {
        component = collider.GetComponent<T>();
        return component != null;
    }
}