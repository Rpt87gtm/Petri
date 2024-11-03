using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInCircle : NetworkBehaviour
{
    [SerializeField] private float _radius = 2f;
    
    private PlayerCells _cells;
    private PlayerInput _playerInput;
    private Vector2 _moveInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _cells = GetComponentInParent<PlayerCells>();
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
            CmdMove(_cells.CenterPosition(),_moveInput);
        }
    }

    [Command]
    private void CmdMove(Vector3 centerPosition, Vector2 input)
    {
        Move(centerPosition,input);
    }
    [Server]
    private void Move(Vector3 centerPosition, Vector2 input)
    {
        Vector3 direction = new Vector3(input.x, input.y, 0f).normalized;
        Vector3 targetPosition = centerPosition + direction * _radius;
        
        transform.position = targetPosition;
        RpcSyncPosition(targetPosition);
    }
    [ClientRpc]
    private void RpcSyncPosition(Vector3 position)
    {
        transform.position = position;
    }
}
