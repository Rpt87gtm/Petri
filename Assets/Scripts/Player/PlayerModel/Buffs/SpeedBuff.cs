using System;

namespace Assets.Scripts.Player.PlayerModel.Buffs
{
    public class SpeedBuff : IBuff
    {
        private float _buffValue;

        public SpeedBuff(float buffValue)
        {
            _buffValue = buffValue;
        }

        public PlayerCellStats ApplyBuff(PlayerCellStats cellStats)
        {
            float newMoveSpeed = Math.Max(cellStats.MoveSpeed + _buffValue, 0);
            return cellStats.WithMoveSpeed(newMoveSpeed);
        }
    }
}
