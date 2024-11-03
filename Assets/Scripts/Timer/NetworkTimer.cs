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
        Debug.Log("start on server");
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
            RpcOnTimerFinished();
            Debug.Log("Timer finished on server, destroying object");
            NetworkServer.Destroy(gameObject);
        }
    }

    //[ClientRpc]
    private void RpcOnTimerFinished()
    {
        Debug.Log("RpcOnTimerFinished called on client");
        TimerFinished?.Invoke();
    }
}