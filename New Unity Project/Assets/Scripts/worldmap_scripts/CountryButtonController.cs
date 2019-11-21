using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryButtonController
{
    public CountryButtonController()
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
        CountryButton conutryButton = new CountryButton(newbutton, worldmap_svg.transform.Find(name).gameObject);
        newbutton.GetComponent<Button>().onClick.AddListener(() => { TaskOnClick(conutryButton); });
    }

    void TaskOnClick(CountryButton country)
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

    class CountryButton
    {   
        GameObject btn;         // 버튼
        GameObject obj;
        public SVGImage svg;
        public string name;
        public string state;
        // Image nationalFlag   // 국가 이미지
           
        /// <summary>
        /// 국가 그림과 버튼 연결
        /// </summary>
        /// <param name="btn">버튼</param>
        /// <param name="obj">국가</param>
        public CountryButton(GameObject btn, GameObject obj)
        {
            this.btn = btn;
            this.obj = obj;
            svg = obj.GetComponent<SVGImage>();
            name = obj.name;
            // 초록
            svg.color = new Color32(0, 255, 0, 255);
            state = "not clicked";
        }

        public void BtnOnClick()
        {
            svg.color = new Color32(255, 0, 0, 255);
            btn.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            btn.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);

            // 해당 위치로 옮겨
            // ViewMove(GameObject.Find("Content").GetComponent<RectTransform>(), obj.GetComponent<RectTransform>().localPosition);
            // GameObject.Find("Content").GetComponent<RectTransform>().localPosition = obj.GetComponent<RectTransform>().localPosition;
            Vector3 contentPos = GameObject.Find("Content").GetComponent<RectTransform>().position;
            Vector3 objPos = obj.GetComponent<RectTransform>().position;
            Debug.Log("TEST Content: " + contentPos);
            Debug.Log("TEST Object: " + objPos);

            // 고정 편차
            Vector3 center = new Vector3(507, 230, 0) + contentPos;

            // 위치 계산
            GameObject.Find("Content").GetComponent<RectTransform>().position = center-objPos;
            state = "clicked";
        }

        public void AgainClick()
        {
            // 초록
            svg.color = new Color32(0, 255, 0, 255);
            btn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            btn.transform.GetChild(0).GetComponent<Text>().color = new Color32(50, 50, 50, 255);

            state = "not clicked";
        }

        IEnumerator ViewMove(RectTransform rt, Vector3 targetPos)
        {
            float step = 0;
            while (step < 1)
            {
                rt.offsetMin = Vector2.Lerp(rt.offsetMin, targetPos, step += Time.deltaTime);
                rt.offsetMax = Vector2.Lerp(rt.offsetMax, targetPos, step += Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
