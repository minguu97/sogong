using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class InputAnyKey : MonoBehaviour
{
    VideoPlayer vd;

    void Start()
    {
        vd = GetComponentInChildren<VideoPlayer>();
    }
    
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("시작");
        }
        if(!vd.isPlaying)
        {
            SceneManager.LoadScene("시작");
        }
    }
}
