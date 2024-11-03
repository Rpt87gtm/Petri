using Mirror;
using System;
using UnityEngine;

public class Mass : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnMassChanged))]
    [SerializeField] private int _value = 0;

    public Action<int> MassChanged;

    [Server]
    public void SetMass(int value)
    {
        _value = value;
    }
    [Server]
    public int GetMass()
    {
        return _value;
    }
    [Server]
    public void IncreaseMass(int amount)
    {
        _value += amount;
    }

    [Server]
    public void DecreaseMass(int amount)
    {
        _value -= amount;
        if (_value < 0)
        {
            _value = 0; 
        }
    }

    [Server]
    public void HalveMass()
    {
        _value /= 2;
    }

    private void OnMassChanged(int oldMass, int newMass)
    {
        MassChanged?.Invoke(_value);
    }
}
