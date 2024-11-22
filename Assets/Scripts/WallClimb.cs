using UnityEngine;

public class WallClimb : MonoBehaviour
{
    [Header("Arc Parameters")]
    [SerializeField] private float _angle = 200f;
    [SerializeField] private int _segments = 10;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Movement Parameters")]
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _rotationSpeed = 200;
    [SerializeField] private float _posYOffset = 0.6f;
    [SerializeField] private Rigidbody2D _rb;

    private float _radius;

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    void Walk()
    {
        _radius = Input.GetAxis("Horizontal");
        RaycastHit2D hit = Physics2DExtensions.ArcCast(transform.position, transform.rotation, _angle, _radius, _segments, _groundLayer);

        if (hit.collider != null && Mathf.Abs(_radius) > 0.9f)
        {
            // Move to new position
            transform.position = Vector2.Lerp(transform.position, hit.point + hit.normal.normalized * _posYOffset, Time.deltaTime * _moveSpeed);

            // Rotate 
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

        }
    }
}
