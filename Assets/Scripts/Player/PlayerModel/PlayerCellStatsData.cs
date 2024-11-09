using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerCellStatsData", menuName = "Player/Player Cell Stats Data", order = 1)]
public class PlayerCellStatsData : ScriptableObject
{
    [SerializeField] private int mass;
    [SerializeField] private float maxGass;
    [SerializeField] private float gassRecovery;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isImmortal;

    public PlayerCellStats CreatePlayerCellStats()
    {
        return new PlayerCellStats(mass, maxGass, gassRecovery, moveSpeed, isImmortal);
    }
}