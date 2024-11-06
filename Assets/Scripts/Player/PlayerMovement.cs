using Assets.Scripts.Player.PlayerModel;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _stopDistance = 0.1f;
    [SerializeField] private float _smoothTime = 0.1f;

    private float _moveSpeed;
    private PlayerCell _playerCell;
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity = Vector2.zero;

    private void Awake()
    {
        _playerCell = GetComponent<PlayerCell>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _moveSpeed = _playerCell.GetCurrentStats().MoveSpeed;
    }

    private void FixedUpdate()
    {
        // Только сервер обрабатывает физику
        if (!isServer) return;

        if (_target == null) return;

        _moveSpeed = _playerCell.GetCurrentStats().MoveSpeed;

        Vector2 direction = (_target.position - transform.position).normalized;
        
        float distanceToTarget = Vector2.Distance(transform.position, _target.position);

        if (distanceToTarget <= _stopDistance)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            Vector2 desiredVelocity = direction * _moveSpeed;
            _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, desiredVelocity, _smoothTime);
        }
        

        // Синхронизируем положение и скорость для клиентов
        RpcSyncPositionAndVelocity(_rigidbody.position, _rigidbody.velocity);
    }

    [ClientRpc]
    private void RpcSyncPositionAndVelocity(Vector2 position, Vector2 velocity)
    {
        // Серверу не нужно обновлять себя
        if (isServer) return;

        // Обновляем позицию и скорость на клиентах, убираем дрожание
        _rigidbody.position = position;
        _rigidbody.velocity = velocity;
    }

    [Command]
    public void CmdSetTarget(Vector3 targetPosition)
    {
        // На сервере устанавливаем цель
        if (_target == null)
        {
            _target = new GameObject("Target").transform;
        }
        _target.position = targetPosition;
    }

    public void SetTarget(Transform target)
    {
        if (isLocalPlayer)
        {
            CmdSetTarget(target.position);
        }
    }
}
