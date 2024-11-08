using UnityEngine;
using Mirror;

public class SpeedBuffPool : NetworkQueuePool<SpeedBuffView>
{
    private MonoTimerFactory _timerFactory;
    public SpeedBuffPool(SpeedBuffView prefab, bool autoExpand = true, int count = 0, Transform container = null, MonoTimerFactory timerFactory = null)
        : base(prefab, autoExpand, count, container)
    {
        _timerFactory = timerFactory;
    }


    public SpeedBuffView GetFreeElement()
    {
        SpeedBuffView speedBuffView = base.GetFreeElement();
        speedBuffView.BuffUsed += () => { ReturnToPool(speedBuffView);};
        speedBuffView.Init(_timerFactory);
        NetworkServer.Spawn(speedBuffView.gameObject);
        return speedBuffView;
    }

    public new void ReturnToPool(SpeedBuffView buff)
    {
        buff.UnsubscribeAll();
        base.ReturnToPool(buff);
    }
}
