using System;
using UnityEngine;

public class MonoTimer : MonoBehaviour, ITimer
{
    public event Action TimerFinished;

    private float _remainingTime;
    private bool _isRunning;



    public static MonoTimer CreateTimer(GameObject go)
    {
        return go.AddComponent<MonoTimer>();
    }

    public void StartTimer(float duration)
    {
        _remainingTime = duration;
        _isRunning = true;
    }

    private void Update()
    {
        if (!_isRunning)
            return;

        _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0)
        {
            _remainingTime = 0;
            _isRunning = false;
            TimerFinished?.Invoke();
            Destroy(this);
        }
    }

    

    private void CopyFrom(MonoTimer other)
    {
        _remainingTime = other._remainingTime;
        _isRunning = other._isRunning;
        TimerFinished = other.TimerFinished;
    }
}