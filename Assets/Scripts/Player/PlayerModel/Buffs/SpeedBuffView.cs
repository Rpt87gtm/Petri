using Assets.Scripts.Player.PlayerModel;
using Assets.Scripts.Player.PlayerModel.Buffs;
using Mirror;
using System;
using UnityEngine;

public class SpeedBuffView : NetworkBehaviour
{
    [SerializeField] private float _speedBuff = 1f;
    [SerializeField] private MonoTimerFactory _timerFactory;
    [SerializeField] private bool _isTemporary;
    [SerializeField] private bool _unactiveOnUsing;
    private float _duration = 5f;

    public event Action BuffUsed;
    public bool IsTemporary
    {
        get { return _isTemporary; }
    }

    public float Duration
    {
        get { return _duration; }
        set { _duration = value; }
    }

    public void Init(MonoTimerFactory factroy)
    {
        _timerFactory = factroy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCell playerCell = other.GetComponent<PlayerCell>();
        if(playerCell != null)
        {
            IBuff speedBuff = new SpeedBuff(_speedBuff);
            if (_isTemporary)
            {
                speedBuff = new TemporaryBuff(playerCell,speedBuff,_duration, _timerFactory);
            }
            playerCell.AddBuff(speedBuff);

            BuffUsed?.Invoke();
            if (_unactiveOnUsing)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void UnsubscribeAll()
    {
        BuffUsed = null;
    }
}
