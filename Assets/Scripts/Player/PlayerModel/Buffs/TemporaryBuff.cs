

namespace Assets.Scripts.Player.PlayerModel.Buffs
{
    internal class TemporaryBuff : IBuff
    {
        private readonly IBuffable _owner;
        private readonly IBuff _coreBuff;
        private readonly float _lifeTime;
        private readonly MonoTimerFactory _monoTimerFactory;
        private ITimer _timer;

        public TemporaryBuff(IBuffable owner, IBuff buff, float lifeTime, MonoTimerFactory timerFactory)
        {
            _owner = owner;
            _coreBuff = buff;
            _lifeTime = lifeTime;
            _monoTimerFactory = timerFactory;
        }

        public PlayerCellStats ApplyBuff(PlayerCellStats cellStats)
        {
            var newState = _coreBuff.ApplyBuff(cellStats);

            if (_timer == null)
            {
                _timer = _monoTimerFactory.CreateTimer(_lifeTime, () => { _owner.RemoveBuff(this); _timer = null; });
            }
            return newState;
        }
    }
}
