using Assets.Scripts.Player.PlayerModel;
using Assets.Scripts.Player.PlayerModel.Buffs;
using Mirror;
using System;
using UnityEngine;

public class MoveSpeedBuffTest : NetworkBehaviour
{
    [SerializeField] PlayerCell _player;
    [SerializeField] private float _speedBuff;
    void Start()
    {
        
    }

    private void Update()
    {
        
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.E)) {
                CmdApplyBuff();
            }
    }

}

    private void CmdApplyBuff()
    {
        _player.AddBuff(new SpeedBuff(_speedBuff));
        Debug.Log(_player.GetCurrentStats());
    }
}
