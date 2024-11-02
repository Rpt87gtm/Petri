using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _target;
    [SerializeField] private float _stopDistance = 0.1f; 
    [SerializeField] private float _smoothTime = 0.1f; 

    private Rigidbody2D _rigidbody;
    private Vector2 _velocity = Vector2.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            Vector2 direction = (_target.position - transform.position).normalized;
            float distanceToTarget = Vector2.Distance(transform.position, _target.position);

            if (distanceToTarget <= _stopDistance)
            {
                _rigidbody.velocity = Vector2.zero;
            }
            else
            {
                Vector2 desiredVelocity = direction * _moveSpeed;
                _rigidbody.velocity = Vector2.SmoothDamp(_rigidbody.velocity, desiredVelocity, ref _velocity, _smoothTime);
            }
        }
    }
}