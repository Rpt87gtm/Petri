namespace Assets.Scripts.Player.PlayerModel.Buffs
{
    public interface IBuff
    {
        PlayerCellStats ApplyBuff(PlayerCellStats cellStats);   
    }
}
