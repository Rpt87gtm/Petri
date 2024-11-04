using System;
using UnityEngine;
using Mirror;

public class NetworkTimer : NetworkBehaviour, ITimer
{
    public event Action TimerFinished;

    [SyncVar]
    private float _remainingTime;

    private bool _isRunning;

    public void StartTimer(float duration)
    {
        if (!isServer)
        {
            Debug.LogWarning("StartTimer should only be called on the server.");
            return;
        }

        _remainingTime = duration;
        _isRunning = true;
    }

    private void Update()
    {
        if (!_isRunning || !isServer)
            return;

        _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0)
        {
            _remainingTime = 0;
            _isRunning = false;
            TimerFinished?.Invoke();
        }
    }
    public void UnsubscribeAll()
    {
        TimerFinished = null;
    }
}