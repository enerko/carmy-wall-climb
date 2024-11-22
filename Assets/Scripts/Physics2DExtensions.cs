using UnityEngine;

public static class Physics2DExtensions
{
    private static float _radiusScalar = 0.7f;
    public static RaycastHit2D ArcCast(Vector2 center, Quaternion rotation, float angle, float radius, int resolution, LayerMask layer)
    {
        RaycastHit2D hit = new RaycastHit2D();

        float directionMultiplier = Mathf.Sign(radius);

        rotation *= Quaternion.Euler(0, 0, directionMultiplier * angle / 2);
        for (int i = 0; i <= resolution; i++) // For each segment
        {
            Vector2 v1 = center + (Vector2)(rotation * Vector2.right * radius * _radiusScalar); // Position of the starting point of the segment
            rotation *= Quaternion.Euler(0, 0, -directionMultiplier * angle / resolution);
            Vector2 v2 = center + (Vector2)(rotation * Vector2.right * radius * _radiusScalar); // Position of the ending point of the segment
            Vector2 direction = v2 - v1;

            hit = Physics2D.Raycast(v1, direction, direction.magnitude * 1.001f, layer);
            Debug.DrawRay(v1, direction, Color.red, 0.1f);

            if (hit.collider != null) return hit;
        }

        return hit;
    }
}


