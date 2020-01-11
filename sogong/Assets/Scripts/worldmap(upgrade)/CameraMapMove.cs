using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMapMove : MonoBehaviour
{
    // 줌 속도
    public float zoomSpeed = 0.5f;

    // 확대 축소 제한
    public float orthographicSizeMin = 0.5f;
    public float orthographicSizeMax = 12f;

    // 카메라 이동 제한
    private float outerLeft = -13f;
    private float outerRight = 13f;
    private float outerBottom = -5f;
    private float outerTop = 5f;

    // 카메라 z좌표 고정
    private float z = -10;

    private Vector3 MouseStart;
    private Camera myCamera;

    void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        // 마우스가 UI(scroll view 등) 위이면 클릭, 휠 등 작동 안되게
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // 마우스 드래그 이동
        if (Input.GetMouseButtonDown(0))
        {
            MouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, z);
            MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
            MouseStart.z = transform.position.z;
        }
        else if (Input.GetMouseButton(0))
        {
            var MouseMove = new Vector3(Input.mousePosition.x, Input.mousePosition.y, z);
            MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);

            MouseMove.z = transform.position.z;
            var newPosition = transform.position - (MouseMove - MouseStart);

            // 영역 제한
            if (newPosition.x > outerRight) newPosition.x = outerRight;
            if (newPosition.x < outerLeft) newPosition.x = outerLeft;
            if (newPosition.y > outerTop) newPosition.y = outerTop;
            if (newPosition.y < outerBottom) newPosition.y = outerBottom;

            transform.position = newPosition;
        }

        // 마우스 휠 줌
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            myCamera.orthographicSize += zoomSpeed;
            myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, orthographicSizeMin, orthographicSizeMax);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            myCamera.orthographicSize -= zoomSpeed;
            myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, orthographicSizeMin, orthographicSizeMax);
        }
    }
}