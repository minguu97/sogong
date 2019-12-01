using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteOnClick : MonoBehaviour
{
    CountryButtonAdapter obj;
    CountryButtonAdapterController controller;
    Vector3 MousePosition = Vector3.zero;

    public void setButton(CountryButtonAdapter obj)
    {
        this.obj = obj;
    }
    private void OnMouseDown()
    {
        MousePosition = Input.mousePosition;
    }
    private void OnMouseUpAsButton()
    {
        if(Input.mousePosition == MousePosition)
            controller.TaskOnClick(obj);
    }

    void Start()
    {
        controller = GameObject.Find("Main Camera").GetComponent<CountryButtonAdapterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
