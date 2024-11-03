using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInCircle : NetworkBehaviour
{
    [SerializeField] private float _radius = 2f;
    
    private PlayerCells _cells;
    private PlayerInput _playerInput;
    private Vector2 _moveInput;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        _playerInput = new PlayerInput();
        _cells = GetComponentInParent<PlayerCells>();
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        _spriteRenderer.enabled = true; 
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
            Vector3 centerPosition = _cells.CenterPosition();
            Vector3 direction = new Vector3(_moveInput.x, _moveInput.y, 0f).normalized;
            Vector3 targetPosition = centerPosition + direction * _radius;

            CmdMove(targetPosition);
            transform.position = targetPosition;
        }
    }

    [Command]
    private void CmdMove(Vector3 targetPosition)
    {
        RpcSyncPosition(targetPosition);
    }
    
    [ClientRpc]
    private void RpcSyncPosition(Vector3 position)
    {
        transform.position = position;
    }
}
