using Mirror;
using UnityEngine;

public class NetworkTimerTest : NetworkBehaviour
{
    [SerializeField] private NetworkTimerFactory _timerFactory;

    private SpriteRenderer _sprite;

    [SyncVar(hook = nameof(OnColorChanged))]
    private Color _playerColor;

    private void Awake()
    {
        _timerFactory = FindObjectOfType<NetworkTimerFactory>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (isServer)
        {
            _playerColor = new Color(Random.value, Random.value, Random.value);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = _playerColor;
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        if (_sprite != null)
        {
            _sprite.color = newColor;
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            CmdCreateTimer(2f);
        }
    }

    [Command]
    private void CmdCreateTimer(float duration)
    {
        if (!isServer) return;

        _timerFactory.CreateTimer(duration, OnTimerFinished);
    }

    private void OnTimerFinished()
    {
        if (isServer)
        {
            _playerColor = new Color(Random.value, Random.value, Random.value);
        }
    }
}
