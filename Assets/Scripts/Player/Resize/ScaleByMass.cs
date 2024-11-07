using Assets.Scripts.Player.PlayerModel;
using UnityEngine;

public class ScaleByMass : MonoBehaviour
{
    [Header("Resize game object")]
    [SerializeField] private GameObject _target;
    [SerializeField] private PlayerCell _playerCell;

    [Header("Scale stats")]
    [SerializeField] private float _startScale;
    [SerializeField] private float _scalePerMass;

    private void Update()
    {
        float newScale = _startScale + _scalePerMass * _playerCell.GetCurrentStats().Mass;
        _target.transform.localScale = new Vector3(newScale, newScale, _target.transform.localScale.z);
    }
}
