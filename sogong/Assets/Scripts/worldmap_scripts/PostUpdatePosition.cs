using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostUpdatePosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // 섬나라 위치 강제조정
        GameObject.Find("morocco(Clone)").transform.localPosition = new Vector3(0f, 0f, 0);
        GameObject.Find("sudan(Clone)").transform.localPosition = new Vector3(881f, 363.39f, 0);
        GameObject.Find("japan(Clone)").transform.localPosition = new Vector3(1645.1f, 445.27f, 0);
        GameObject.Find("kyrgyzstan(Clone)").transform.localPosition = new Vector3(1082.5f, 525.6f, 0);
        GameObject.Find("moldova(Clone)").transform.localPosition = new Vector3(894.66f, 558.32f, 0);
    }
}
