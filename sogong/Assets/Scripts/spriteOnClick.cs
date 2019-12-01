using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteOnClick : MonoBehaviour
{
    CountryButtonAdapter obj;
    CountryButtonAdapterController controller;
    public void setButton(CountryButtonAdapter obj)
    {
        this.obj = obj;
    }
    private void OnMouseUpAsButton()
    {
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
