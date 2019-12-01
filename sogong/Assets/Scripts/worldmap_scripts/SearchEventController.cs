using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SearchEventController : MonoBehaviour
{
    public Text text;
    Button search;
    List<Country> conutryList;
    CountryButtonAdapterController controller;


    // Start is called before the first frame update
    void Start()
    {
        conutryList = CountryFileReader.GetInstance().GetList();

    }

    public void ButtonClicked()
    {
        controller = GameObject.Find("Main Camera").GetComponent<CountryButtonAdapterController>();
        Debug.Log(controller.s);
        text = GameObject.Find("입력").GetComponent<Text>();
        foreach (Country c in conutryList)
        {
            string comp = text.text;
            Debug.Log("TEST comp: " + comp);
            if (comp == c.getKoreanName())
                controller.TaskOnClick(c.getAdapter());

        }
    }
}
