using Assets.Scripts.Player.PlayerModel;
using Assets.Scripts.Player.PlayerModel.Buffs;
using UnityEngine;

public class SpeedBuffView : MonoBehaviour
{
    [SerializeField] private float _speedBuff = 1f;
    [SerializeField] private MonoTimerFactory _timerFactory;
    [SerializeField] private bool _isTemporary;
    private float _duration = 5f;
    public bool IsTemporary
    {
        get { return _isTemporary; }
    }

    public float Duration
    {
        get { return _duration; }
        set { _duration = value; }
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
        }
    }
}