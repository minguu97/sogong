using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostUpdatePosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 섬나라 위치 강제조정
        GameObject.Find("morocco(Clone)").transform.localPosition = new Vector3(702.01f, 428.14f, 0);
        GameObject.Find("sudan(Clone)").transform.localPosition = new Vector3(881f, 363.39f, 0);
        GameObject.Find("japan(Clone)").transform.localPosition = new Vector3(1345.1f, 445.27f, 0);
        GameObject.Find("kyrgyzstan(Clone)").transform.localPosition = new Vector3(1082.5f, 525.6f, 0);
        GameObject.Find("moldova(Clone)").transform.localPosition = new Vector3(894.66f, 558.32f, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
