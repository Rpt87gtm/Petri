using System;

[Serializable]
public struct PlayerCellStats {
    public readonly int Mass;
    public readonly float MoveSpeed;
    public readonly bool IsImmortal;

    public PlayerCellStats(int mass, float moveSpeed, bool isImmortal)
    {
        Mass = mass;
        MoveSpeed = moveSpeed;
        IsImmortal = isImmortal;

    }
    public PlayerCellStats WithMass(int mass)
    {
        return new PlayerCellStats(mass, MoveSpeed, IsImmortal);
    }

    public PlayerCellStats WithMoveSpeed(float moveSpeed)
    {
        return new PlayerCellStats(Mass, moveSpeed, IsImmortal);
    }

    public PlayerCellStats WithIsImmortal(bool isImmortal)
    {
        return new PlayerCellStats(Mass, MoveSpeed, isImmortal);
    }

}
