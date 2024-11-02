using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset = new Vector3(0,0,-10f);

    private Vector3 _velocity = Vector3.zero;
    private void LateUpdate()
    {
       
        if (_target != null)
        {
            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}