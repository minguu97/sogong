using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button close = transform.Find("Close").GetComponent<Button>();
        close.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("Close the information");
        Destroy(gameObject);
        GameObject.Find("ocean").GetComponent<NationController>().clickedFlag = null;
    }
}
