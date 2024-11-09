using TMPro;
using UnityEngine;

namespace Assets.Scripts.Player.PlayerModel
{
    public class PlayerStatsDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerCell _playerCell;
        [SerializeField] private TextMeshProUGUI _textMesh;

        private void Start()
        {
            if (_playerCell != null && _playerCell.isLocalPlayer)
            {
                _textMesh.gameObject.SetActive(true);
            }
            else
            {
                _textMesh.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (!_playerCell.isLocalPlayer) return;
            if (_playerCell != null && _textMesh != null)
            {
                PlayerCellStats stats = _playerCell.GetCurrentStats();
                _textMesh.text = $"Mass: {stats.Mass}\n" +
                                $"Max Gass: {stats.MaxGass}\n" +
                                $"Gass Recovery: {stats.GassRecovery}\n" +
                                $"Move Speed: {stats.MoveSpeed}\n" +
                                $"Is Immortal: {stats.IsImmortal}";
            }
        }
    }
}