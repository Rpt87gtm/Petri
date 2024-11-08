using System;
using UnityEngine;

public class NetworkTimerPool : NetworkQueuePool<NetworkTimer>
{
    public NetworkTimerPool(NetworkTimer timerPrefab, bool autoExpand = true, int initialCount = 5, Transform container = null)
        : base(timerPrefab, autoExpand, initialCount, container)
    {
    }

    public NetworkTimer GetTimer(float duration, Action onFinished)
    {
        NetworkTimer timer = GetFreeElement(true);

        timer.StartTimer(duration);
        timer.TimerFinished += onFinished;
        timer.TimerFinished += () => ReturnToPool(timer);  

        return timer;
    }

    private new void ReturnToPool(NetworkTimer timer)
    {
        timer.UnsubscribeAll();
        base.ReturnToPool(timer);  
    }
}
