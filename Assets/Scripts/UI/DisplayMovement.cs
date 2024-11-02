using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisplayMovement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _movementText;

    private PlayerInput _playerInputActions;
    private Vector2 _moveInput;

    private void Awake()
    {
        _playerInputActions = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
        _playerInputActions.Player.Move.performed += OnMovePerformed;
        _playerInputActions.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Move.performed -= OnMovePerformed;
        _playerInputActions.Player.Move.canceled -= OnMoveCanceled;
        _playerInputActions.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        UpdateMovementText();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
        UpdateMovementText();
    }

    private void UpdateMovementText()
    {
        _movementText.text = $"Movement Vector: {_moveInput}";
    }
}
