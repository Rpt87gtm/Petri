using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MonoTimerFactory : NetworkBehaviour
{
    [SerializeField] private GameObject _timerPrefab;
    [SerializeField] private Transform _container;

    private Queue<MonoTimer> _timerPool = new Queue<MonoTimer>();

    public MonoTimer CreateReturningTimer(float duration, Action onFinished)
    {
        MonoTimer timer = CreateTimer();

        timer.StartTimer(duration);
        timer.TimerFinished += onFinished;
        timer.TimerFinished += () => OnTimerFinished(timer);

        return timer;
    }

    public MonoTimer CreateTimer() {
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
            GameObject timerObject = Instantiate(_timerPrefab, _container);
            timer = timerObject.GetComponent<MonoTimer>();
            if (timer == null)
            {
                Debug.LogError("Failed to get NetworkTimer component from prefab.");
                Destroy(timerObject);
                return null;
            }

        }
        return timer;
    }

    private void OnTimerFinished(MonoTimer timer)
    {
        ReturnTimerToPool(timer);
    }

    public void ReturnTimerToPool(MonoTimer timer)
    {
        timer.gameObject.SetActive(false);
        timer.UnsubscribeAll();
        _timerPool.Enqueue(timer);
    } 

    public void ClearPool()
    {
        MonoTimer timer = _timerPool.Dequeue();
        Destroy(timer.gameObject);
    }
}
