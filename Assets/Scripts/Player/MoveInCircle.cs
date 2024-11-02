using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInCircle : NetworkBehaviour
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
        if (isServer)
        {
            _centerPosition = transform.localPosition;
        }
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += OnMovePerformed;
        _playerInput.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= OnMovePerformed;
        _playerInput.Player.Move.canceled -= OnMoveCanceled;
        _playerInput.Disable();
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }
    private void Update()
    {
        if (isLocalPlayer)
        {
            CmdMove(_moveInput);
        }
    }

    [Command]
    private void CmdMove(Vector2 input)
    {
        Move(input);
    }
    [Server]
    private void Move(Vector2 input)
    {
        Vector3 direction = new Vector3(input.x, input.y, 0f).normalized;
        Vector3 targetPosition = _centerPosition + direction * _radius;
        transform.localPosition = targetPosition;
        RpcSyncPosition(targetPosition);
    }
    [ClientRpc]
    private void RpcSyncPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
