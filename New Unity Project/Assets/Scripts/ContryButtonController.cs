using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContryButtonController : MonoBehaviour
{
    ArrayList country;

    void Start()
    {
        // 나라 리스트
        country = new ArrayList(new string[] { "china", "japan"  });

        foreach (string str in country)
        {
            string btn_name = "btn_" + str;
            GameObject buttonPrefab = GameObject.Find("btn_country");
            
            // 리스트마다 버튼 생성
            CreateButton(str, buttonPrefab, GameObject.Find("Content_btnCountry").transform);
        }
    }

    public void CreateButton(string name, GameObject prefab, Transform panel)
    {
        GameObject button = (GameObject)Instantiate(prefab);
        CountryButton countrybutton = new CountryButton(name);

        button.transform.SetParent(panel);                                                          // parent 정해서 위치 정하고
        button.GetComponent<Button>().onClick.AddListener(() => { TaskOnClick(countrybutton); });   // 리스너 추가하고
        // TODO 매치되는 나라 한글이름 List 필요
        button.transform.GetChild(0).GetComponent<Text>().text = name;                              // 이름 설정하고 (현재는 영어이름 그대로 씀.
        button.name = "btn_" + name;                                                                // id 설정하고
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
    }

    class CountryButton
    {
        public SVGImage svg;
        public string name;
        public string state;
        // Image nationalFlag   // 국가 이미지

        public CountryButton(string str)
        {
            svg = GameObject.Find(str).GetComponent<SVGImage>();
            name = svg.name;
            // 투명
            svg.color = new Color32(0, 0, 0, 0);
            state = "not clicked";
        }

        public void BtnOnClick()
        {
            // 빨강
            svg.color = new Color32(255, 0, 0, 255);
            state = "clicked";
        }

        public void AgainClick()
        {
            // 지도 색(초록)
            svg.color = new Color32(82, 152, 63, 0);
            state = "not clicked";
        }
    }
}
