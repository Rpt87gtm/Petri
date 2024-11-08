using System;

namespace Assets.Scripts.Player.PlayerModel.Buffs
{
    internal class TemporaryBuff : IBuff
    {
        private readonly IBuffable _owner;
        private readonly IBuff _coreBuff;
        private readonly float _lifeTime;
        private ITimer _timer;

        public TemporaryBuff(IBuffable owner, IBuff buff, float lifeTime, ITimer timer)
        {
            _owner = owner;
            _coreBuff = buff;
            _lifeTime = lifeTime;
            _timer = timer;
        }

        public PlayerCellStats ApplyBuff(PlayerCellStats cellStats)
        {
            var newState = _coreBuff.ApplyBuff(cellStats);

            _timer.StartTimer(_lifeTime);
            _timer.TimerFinished += () =>
            {
                _owner.RemoveBuff(this);
            };

            return newState;
        }
    }
}
