using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Assets.Scripts.Player.PlayerModel.Buffs;

namespace Assets.Scripts.Player.PlayerModel
{
    public class PlayerCell : NetworkBehaviour, IBuffable
    {
        [SyncVar] private PlayerCellStats currentStats;
        private List<IBuff> activeBuffs = new List<IBuff>();

        [SerializeField] private PlayerCellStatsData baseStatsData;
        private PlayerCellStats baseStats;

        private void Awake()
        {
            baseStats = baseStatsData.CreatePlayerCellStats();
            currentStats = baseStats;
        }

        private void Start()
        {
            IBuff foodBuff = GetComponent<EatenFoodBuff>();
            AddBuff(foodBuff);
        }
        private void Update()
        {
            RecalculateStats();
        }
        public PlayerCellStats GetCurrentStats()
        {
            return currentStats;
        }

        public void AddBuff(IBuff buff)
        {
            if (!isServer) return; 
            activeBuffs.Add(buff);
            RecalculateStats();
        }

        public void RemoveBuff(IBuff buff)
        {
            if (!isServer) return; 
            activeBuffs.Remove(buff);
            RecalculateStats();
        }

        [Server]
        private void RecalculateStats()
        {
            PlayerCellStats updatedStats = baseStats;
            foreach (var buff in activeBuffs)
            {
                updatedStats = buff.ApplyBuff(updatedStats);
            }
            currentStats = updatedStats;
            RpcSyncStats(currentStats); 
        }

        [ClientRpc]
        private void RpcSyncStats(PlayerCellStats newStats)
        {
            currentStats = newStats;
        }
    }
}
