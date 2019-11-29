using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class movieCheck : MonoBehaviour
{
    VideoPlayer test;
    bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        test = GetComponentInChildren<VideoPlayer>();
        // Debug.Log(test.GetType());
    }

    // Update is called once per frame
    void Update()
    {
        if (test.isPlaying)
        {
            started = true;
        }
        if(started == true && !test.isPlaying)
        {
            SceneManager.LoadScene("시작");
        }
    }
}
