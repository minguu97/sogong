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
        //// UI 위에서 클릭은 지도가 클릭 안되도록 추가
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (results.Count > 0)
        {
            return;
        }
        ////

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
