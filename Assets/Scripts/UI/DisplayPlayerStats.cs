using TMPro;
using UnityEngine;

namespace Assets.Scripts.Player.PlayerModel
{
    public class PlayerStatsDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerCell playerCell;
        [SerializeField] private TextMeshProUGUI textMesh;

        private void Update()
        {
            if (playerCell != null && textMesh != null)
            {
                PlayerCellStats stats = playerCell.GetCurrentStats();
                textMesh.text = $"Mass: {stats.Mass}\n" +
                                $"Max Gass: {stats.MaxGass}\n" +
                                $"Gass Recovery: {stats.GassRecovery}\n" +
                                $"Move Speed: {stats.MoveSpeed}\n" +
                                $"Is Immortal: {stats.IsImmortal}";
            }
        }
    }
}