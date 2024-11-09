namespace Assets.Scripts.Player.PlayerModel.Buffs
{
    public interface IBuffable
    {
        void AddBuff(IBuff buff);
        void RemoveBuff(IBuff buff);
        
    }
}
