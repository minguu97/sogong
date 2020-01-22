using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClearText : MonoBehaviour, IPointerClickHandler
{
    InputField ifield;
    private void Start()
    {
        ifield = this.gameObject.GetComponent<InputField>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ifield.text = "";
    }
}