using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    int Width = 1024;
    int Height = 768;
    bool fullscreen = true;
    void Start()
    {
        Screen.SetResolution(Width, Height, true);
    }

    void Update()
    {
        
    }
}
