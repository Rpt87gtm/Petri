using System;

[Serializable]
public struct PlayerCellStats {
    public readonly int Mass;
    public readonly float Gass;
    public readonly float MoveSpeed;
    public readonly bool IsImmortal;

    public PlayerCellStats(int mass, float gass, float moveSpeed, bool isImmortal)
    {
        Mass = mass;
        Gass = gass;
        MoveSpeed = moveSpeed;
        IsImmortal = isImmortal;

    }
    public PlayerCellStats WithMass(int mass)
    {
        return new PlayerCellStats(mass,Gass, MoveSpeed, IsImmortal);
    }

    public PlayerCellStats WithMoveSpeed(float moveSpeed)
    {
        return new PlayerCellStats(Mass, Gass, moveSpeed, IsImmortal);
    }

    public PlayerCellStats WithIsImmortal(bool isImmortal)
    {
        return new PlayerCellStats(Mass, Gass, MoveSpeed, isImmortal);
    }

    public PlayerCellStats WithGass(float gass)
    {
        return new PlayerCellStats(Mass, gass, MoveSpeed, IsImmortal);
    }
}
