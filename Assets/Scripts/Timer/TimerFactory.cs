using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class TimerFactory : NetworkBehaviour
{
    [SerializeField] private GameObject _timerPrefab;

    private Queue<NetworkTimer> _timerPool = new Queue<NetworkTimer>();

    public NetworkTimer CreateTimer(float duration, Action onFinished)
    {
        if (!isServer)
        {
            Debug.LogWarning("CreateTimer should only be called on the server.");
            return null;
        }

        NetworkTimer timer;

        if (_timerPool.Count > 0)
        {
            timer = _timerPool.Dequeue();
            timer.gameObject.SetActive(true);
        }
        else
        {
            GameObject timerObject = Instantiate(_timerPrefab);
            timer = timerObject.GetComponent<NetworkTimer>();
            if (timer == null)
            {
                Debug.LogError("Failed to get NetworkTimer component from prefab.");
                Destroy(timerObject);
                return null;
            }

            NetworkServer.Spawn(timerObject);
        }

        timer.StartTimer(duration);
        timer.TimerFinished += onFinished;
        timer.TimerFinished += () => ReturnTimerToPool(timer);

        return timer;
    }

    private void ReturnTimerToPool(NetworkTimer timer)
    {
        timer.gameObject.SetActive(false);
        timer.UnsubscribeAll();
        _timerPool.Enqueue(timer);
    }
}
