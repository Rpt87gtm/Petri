using System;

[Serializable]
public record PlayerCellStats {
    public readonly int Mass;
    public readonly float MaxGass;
    public readonly float GassRecovery;
    public readonly float MoveSpeed;
    public readonly bool IsImmortal;

    public PlayerCellStats(int mass, float maxGass, float gassRecovery, float moveSpeed, bool isImmortal)
    {
        Mass = mass;
        MaxGass = maxGass;
        GassRecovery = gassRecovery;
        MoveSpeed = moveSpeed;
        IsImmortal = isImmortal;

    }

    public PlayerCellStats()
    {
        Mass = 0;
        MaxGass = 0f;
        GassRecovery = 0f;
        MoveSpeed = 0f;
        IsImmortal = false;
    }

    public PlayerCellStats WithMass(int mass)
    {
        return new PlayerCellStats(mass,MaxGass,GassRecovery, MoveSpeed, IsImmortal);
    }

    public PlayerCellStats WithMoveSpeed(float moveSpeed)
    {
        return new PlayerCellStats(Mass, MaxGass,GassRecovery, moveSpeed, IsImmortal);
    }

    public PlayerCellStats WithIsImmortal(bool isImmortal)
    {
        return new PlayerCellStats(Mass, MaxGass,GassRecovery, MoveSpeed, isImmortal);
    }

    public PlayerCellStats WithMaxGass(float maxGass)
    {
        return new PlayerCellStats(Mass, maxGass,GassRecovery, MoveSpeed, IsImmortal);
    }
    public PlayerCellStats WithGassRecovery(float gassRecovery)
    {
        return new PlayerCellStats(Mass, MaxGass, gassRecovery, MoveSpeed, IsImmortal);
    }


    internal PlayerCellStats Copy()
    {
        return new PlayerCellStats(Mass, MaxGass,GassRecovery, MoveSpeed, IsImmortal);
    }
}
