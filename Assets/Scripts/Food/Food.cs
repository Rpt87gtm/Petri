using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int _nutritional;
    public event Action Eaten;

    public int GetNutritional()
    {
        return _nutritional;
    }

    public int Eat() {  
        Eaten?.Invoke();
        return _nutritional;
    }
}
