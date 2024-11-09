using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerCellsGroup _playerCells;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset = new Vector3(0,0,-10f);

    private Vector3 _velocity = Vector3.zero;
    private void LateUpdate()
    {
       
        if (_playerCells != null)
        {
            Vector3 desiredPosition = _playerCells.CenterPosition() + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = desiredPosition;
        }
    }

    public void SetTarget(PlayerCellsGroup playerCells)
    {
        _playerCells = playerCells;
    }
}