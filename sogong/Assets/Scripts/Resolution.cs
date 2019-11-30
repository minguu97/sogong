using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    int SetWidth = 1024;
    int SetHeight = 768;
    bool fullscreen = true;  // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(Screen.width, Screen.height, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
