using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryButtonAdapterController : MonoBehaviour
{
    public CountryButtonAdapterController()
    {
        CreateButtons();
    }

    public void CreateButtons()
    {
        List<Country> conutryList = CountryFileReader.GetInstance().GetList();
        foreach (Country c in conutryList)
        {
            string str = c.getName();
            string btn_name = "btn_" + str;
            GameObject buttonPrefab = Resources.Load("Prefabs/btnInterface_country") as GameObject; ;

            // 리스트마다 버튼 생성. parent: Viewport_CountryList
            CreateButton(str, buttonPrefab, GameObject.Find("Content_btnCountry").transform);
        }
    }

    public void CreateButton(string name, GameObject prefab, Transform panel)
    {
        GameObject button = GameObject.Instantiate(prefab);

        // 이름 설정하고 (현재는 영어이름 그대로 씀.)
        button.transform.GetChild(0).GetComponent<Text>().text = name;
        // id 설정하고
        button.name = "btn_" + name;
        // 리스너 설정
        setListner(button);
        // parent 정해서 위치 설정
        button.transform.SetParent(panel);  
    }

    void setListner(GameObject newbutton)
    {
        GameObject worldmap_svg = GameObject.Find("worldmap_svg");
        string name = newbutton.name.Substring(4);
        CountryButtonAdapter conutryButton = new CountryButtonAdapter(
            newbutton,
            worldmap_svg.transform.Find(name + "(Clone)").gameObject);
        newbutton.GetComponent<Button>().onClick.AddListener(() => { TaskOnClick(conutryButton); });
    }

    void TaskOnClick(CountryButtonAdapter country)
    {
        if (country.state == "clicked")
        {
            country.AgainClick();
        } else
        {
            country.BtnOnClick();
        }
        Debug.Log("TEST button: " + country.name + " " + country.state);
    }
}
