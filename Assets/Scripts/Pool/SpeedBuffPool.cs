using UnityEngine;
using Mirror;
using System;

public class SpeedBuffPool : NetworkQueuePool<SpeedBuffView>
{
    private MonoTimerFactory _timerFactory;
    public event Action ReturnedToPool;
    public event Action Spawn;
    public SpeedBuffPool(SpeedBuffView prefab, MonoTimerFactory timerFactory, bool autoExpand = true, int count = 0, Transform container = null)
        : base(prefab, autoExpand, count, container)
    {

        _timerFactory = timerFactory;
    }

    public SpeedBuffPool():base()
    {
    }

    public SpeedBuffView GetFreeElement()
    {
        SpeedBuffView speedBuffView = base.GetFreeElement();
        speedBuffView.BuffUsed += () => { ReturnToPool(speedBuffView);};
        speedBuffView.Init(_timerFactory);
        
        NetworkServer.Spawn(speedBuffView.gameObject);
        Spawn?.Invoke();
        return speedBuffView;
    }

    public new void ReturnToPool(SpeedBuffView buff)
    {
        buff.UnsubscribeAll();
        base.ReturnToPool(buff);
        ReturnedToPool?.Invoke(); 
    }
}
