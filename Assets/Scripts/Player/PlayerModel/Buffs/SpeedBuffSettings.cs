using UnityEngine;

[CreateAssetMenu(fileName = "SpawnBuffSettings", menuName = "Game Settings/Buffs/Buff Spawner Settings")]
public class SpeedBuffSettings : ScriptableObject
{
    [Header("Spawn Settings")]
    public float Radius = 5f;
    public int BuffsCount;

    [Header("Respawn")]
    public int RestawnPerTick = 1;
    public float SecondsBetweenSpawn = 2f;
}
