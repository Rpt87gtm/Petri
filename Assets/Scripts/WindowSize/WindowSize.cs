using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSize : MonoBehaviour
{
    [SerializeField] private int _width = 800;
    [SerializeField] private int _height = 600;
    private void Start()
    {
        Screen.SetResolution(_width, _height,false);
    }
}
