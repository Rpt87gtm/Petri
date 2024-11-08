using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MonoTimerFactory : NetworkBehaviour
{
    [SerializeField] private GameObject _timerPrefab;
    [SerializeField] private Transform _container;

    private Queue<MonoTimer> _timerPool = new Queue<MonoTimer>();

    public MonoTimer CreateTimer(float duration, Action onFinished)
    {
        if (!isServer)
        {
            Debug.LogWarning("CreateTimer should only be called on the server.");
            return null;
        }

        MonoTimer timer;

        if (_timerPool.Count > 0)
        {
            timer = _timerPool.Dequeue();
            timer.gameObject.SetActive(true);
        }
        else
        {
            GameObject timerObject = Instantiate(_timerPrefab,_container);
            timer = timerObject.GetComponent<MonoTimer>();
            if (timer == null)
            {
                Debug.LogError("Failed to get NetworkTimer component from prefab.");
                Destroy(timerObject);
                return null;
            }

        }

        timer.StartTimer(duration);
        timer.TimerFinished += onFinished;
        timer.TimerFinished += () => OnTimerFinished(timer);

        return timer;
    }

    private void OnTimerFinished(MonoTimer timer)
    {
        ReturnTimerToPool(timer);
        RpcNotifyClientsTimerFinished();
    }

    private void ReturnTimerToPool(MonoTimer timer)
    {
        timer.gameObject.SetActive(false);
        timer.UnsubscribeAll();
        _timerPool.Enqueue(timer);
    }

    [ClientRpc]
    private void RpcNotifyClientsTimerFinished()
    {
        Debug.Log("Timer finished on client!");
    }
}