using Mirror;
using UnityEngine;

public class NetworkTimerTest : NetworkBehaviour
{
    [SerializeField] private GameObject _timerPrefab;

    private SpriteRenderer _sprite;

    [SyncVar(hook = nameof(OnColorChanged))]
    private Color _playerColor;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
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
            Debug.Log($"OnColorChanged: {newColor}");
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            CmdCreateTimer(4f);
        }
    }

    [Command]
    private void CmdCreateTimer(float duration)
    {
        Debug.Log("CmdCreateTimer called on server");
        GameObject timerObject = Instantiate(_timerPrefab);
        NetworkTimer timer = timerObject.GetComponent<NetworkTimer>();
        if (timer == null)
        {
            Debug.LogError("Failed to get NetworkTimer component from prefab.");
            Destroy(timerObject);
            return;
        }

        NetworkServer.Spawn(timerObject);
        timer.StartTimer(duration);
        timer.TimerFinished += OnTimerFinished;
    }

    private void OnTimerFinished()
    {
        if (isServer)
        {
            _playerColor = new Color(Random.value, Random.value, Random.value);
        }
        Debug.Log("NetworkTimer finished!");
    }
}