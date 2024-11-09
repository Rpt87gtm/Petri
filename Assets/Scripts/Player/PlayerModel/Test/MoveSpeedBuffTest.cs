using Assets.Scripts.Player.PlayerModel;
using Assets.Scripts.Player.PlayerModel.Buffs;
using Mirror;
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
    [Command]
    private void CmdApplyBuff()
    {
        _player.AddBuff(new SpeedBuff(_speedBuff));
    }
}
