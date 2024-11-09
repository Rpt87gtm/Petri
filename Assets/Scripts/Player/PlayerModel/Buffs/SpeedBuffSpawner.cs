using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedBuffSpawner : NetworkBehaviour
{
    [SerializeField] private SpeedBuffSettings speedBuffSettings;

    [Header("PoolSettings")]
    [SerializeField] private GameObject _prefab;
    [SerializeField] private MonoTimerFactory _timerFactory;
    [SerializeField] private bool _autoExpand;
    [SerializeField] private Transform _container;

    [SyncVar]
    private SpeedBuffPool _buffPool;
    private int _currentBuffCount;
    private ITimer _timer;
    private Circle _circle;
    private bool isInit = false;

    public override void OnStartServer()
    {
        base.OnStartServer();
        if (!isServer) return;
        Init();
        StartRespawn();
    }
    
    public override void OnStopServer()
    {
        StopRespawn();
        base.OnStopServer();
    }
    public void Init()
    {
        _buffPool = new SpeedBuffPool(_prefab.GetComponent<SpeedBuffView>(), _timerFactory, _autoExpand, speedBuffSettings.BuffsCount, _container);
        _currentBuffCount = 0;
        _circle = new Circle(speedBuffSettings.Radius);

        if (_timer == null)
        {
            _timer = _timerFactory.CreateTimer();
        }

        isInit = true;
        _buffPool.Spawn += AddBuffCount;
        _buffPool.ReturnedToPool += IncreaseBuffCount;

        for (int i = 0; i < speedBuffSettings.BuffsCount; i++)
        {
            SpawnBuff();
        }
    }

    public void StartRespawn() {
        _timer.TimerFinished += Respawn;
        RestartTimer();
    }
    public void StopRespawn() {
        _timer.TimerFinished -= Respawn;
    }

    private void Respawn()
    {
        int countToRespawn = Mathf.Min((speedBuffSettings.BuffsCount - _currentBuffCount), speedBuffSettings.RestawnPerTick);

        for (int spawned = 0; spawned < countToRespawn; spawned++)
        {
            SpawnBuff();
        }
        RestartTimer();
    }

    private void RestartTimer() {
        _timer.StartTimer(speedBuffSettings.SecondsBetweenSpawn);
    }

    private void OnEnable()
    {
        if (!isInit) return;
        _buffPool.Spawn += AddBuffCount;
        _buffPool.ReturnedToPool += IncreaseBuffCount;
    }
    private void OnDisable()
    {
        if (!isInit) return;
        _buffPool.Spawn -= AddBuffCount;
        _buffPool.ReturnedToPool -= IncreaseBuffCount;
    }

    private void AddBuffCount()
    {
        _currentBuffCount++;
    }
    private void IncreaseBuffCount()
    {
        _currentBuffCount--;
    }

    private void SpawnBuff()
    {
        SpeedBuffView buff = _buffPool.GetFreeElement();
        buff.transform.position = _circle.GetRandomPointInCircle();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; 
        Gizmos.DrawWireSphere(transform.position, speedBuffSettings.Radius); 
    }
}
