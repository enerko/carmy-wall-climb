using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private float _followDistance = 0.5f;

    void Update()
    {
        // Calculate the direction to move
        Vector2 direction = (Vector2)_followTarget.position - (Vector2)transform.position;

        // Maintain the follow distance
        if (direction.magnitude > _followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _followTarget.position, _followSpeed * Time.deltaTime);
        }

        // Rotate object
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _followSpeed * Time.deltaTime);

    }
}
