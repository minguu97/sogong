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
        country = new ArrayList(new string[] { "china", "japan" });

        foreach (string str in country)
        {
            // 문자열로 버튼 추출
            Button btn = GameObject.Find("btn_" + str).GetComponent<Button>();

            // 이름으로 나라모양 버튼 추출
            CountryButton country = new CountryButton(btn.name.Substring(4));

            // 리스너 등록
            btn.onClick.AddListener(() => { TaskOnClick(country); }) ;
        }

    }

    void TaskOnClick(CountryButton country)
    {
        if (country.state == "clicked")
        {
            country.AgainClicked();
        } else
        {
            country.BtnOnClick();
        }
    }

    // TODO 모든 것을 public으로 쓰지 않으면 에러가 난다.
    public class CountryButton
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

        public void AgainClicked()
        {
            // 지도 색(초록)
            svg.color = new Color32(82, 152, 63, 0);
            state = "not clicked";
        }
    }
}
