using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInCircle : NetworkBehaviour
{
    [SerializeField] private float _radius = 2f;
    
    [SerializeField] private Transform _centerPos;
    private PlayerInput _playerInput;
    private Vector2 _moveInput;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        _playerInput = new PlayerInput();
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

    private void LateUpdate()
    {
        if (isLocalPlayer)
        {
            

            CmdMove(_moveInput);
          
        }
    }

    [Command]
    private void CmdMove(Vector3 moveInput)
    {
        Vector3 centerPosition = _centerPos.position;
        Vector3 direction = new Vector3(moveInput.x, moveInput.y, 0f).normalized;
        Vector3 targetPosition = centerPosition + direction * _radius;
        transform.position = targetPosition;
    }
   
}
