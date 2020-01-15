using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapCallbackListener : MonoBehaviour
{
    private Vector3 mousePosition = Vector3.zero;
    private bool isClicked = false;
    private GameObject targetButton;

    public void setTarget(GameObject button)
    {
        targetButton = button;
    }

    void Start()
    {
        ;   
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition;
        isClicked = true;
    }
    private void OnMouseUp()
    {
        if (Input.mousePosition != mousePosition)
        {
            isClicked = false;
            return;
        }
        else if (!isClicked)
            return;
        else
        {
            isClicked = false;
            runEvent();
        }
    }
    private void OnMouseExit()
    {
        isClicked = false;
    }
    void runEvent()
    {
        //targetButton;
        ExecuteEvents.Execute(targetButton.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
    }
    
}
