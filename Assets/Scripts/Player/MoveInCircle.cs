using UnityEngine;

public class MoveInCircle : MonoBehaviour
{
    [SerializeField] private float _radius = 2f;

    private Vector3 _centerPosition;
    private PlayerInput _playerInput;
    private Vector2 _moveInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }
    private void Start()
    {
        _centerPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInput.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInput.Player.Move.canceled -= ctx => _moveInput = Vector2.zero;
        _playerInput.Disable();
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(_moveInput.x, _moveInput.y, 0f).normalized;
        Vector3 targetPosition = _centerPosition + direction * _radius;
        transform.localPosition = targetPosition;
    }

}
