using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controls every country.
/// </summary>
public class CountryFactory : MonoBehaviour
{
    void Start()
    {
        List<Country> countryList;

        // Load country data and Get List<Country>
        CountryFileReader reader = CountryFileReader.GetInstance();
        countryList = reader.GetList();

        foreach(Country c in countryList)
        {
            string objName = c.getName();
            GameObject svgObj = GameObject.Find(objName);
            if (! svgObj)
            {
                Debug.Log("'" + objName + "'을 찾을 수 없습니다.");
                continue;
            }
            Sprite spr = svgObj.GetComponent<SpriteRenderer>().sprite;

            // Image 객체로 만들어서 Set Native Size한 후 width, height 구하기
            GameObject sizeChecker = GameObject.Find("SizeChecker");
            Image img = sizeChecker.GetComponent<Image>();
            img.sprite = spr;
            img.SetNativeSize();

            Vector2 size = sizeChecker.GetComponent<RectTransform>().sizeDelta;

            // 새로운 GameObject 생성
            GameObject newCountry = new GameObject(objName);
            var rect = newCountry.AddComponent<RectTransform>();
            var svg = newCountry.AddComponent<SVGImage>();

            // sprite 설정
            svg.sprite = spr;

            // color 설정
            svg.color = new Color32(0, 255, 0, 255);

            // width, height 설정
            rect.sizeDelta = size;

            // AnchorPresets을 bottom, left로 설정
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);

            // Pivot 0, 0으로 설정
            rect.pivot = new Vector2(0, 0);

            // Scale 4, 4로 설정
            rect.localScale = new Vector3(8f, 8f, 1f);

            // location을 x, y좌표 설정
            rect.localPosition = c.getLocation();

            // worldmap_svg (0번 자식) 밑으로 위치
            newCountry.transform.SetParent(this.transform.GetChild(0));
        }

        // 비율 조정
        CountryCoordinateCalculator calc = new CountryCoordinateCalculator(countryList, 0.6292f);

        // 버튼 생성
        CountryButtonController controller = new CountryButtonController();
    }
}
