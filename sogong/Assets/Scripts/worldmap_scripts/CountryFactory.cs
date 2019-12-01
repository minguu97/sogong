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
            // 새로운 GameObject 생성
            Object countryPrefab = Resources.Load<Object>("country_svg/" + objName);
            GameObject newCountry = Instantiate(countryPrefab) as GameObject;

            var rect = newCountry.AddComponent<RectTransform>();
            var svg = newCountry.GetComponent<SpriteRenderer>();
            //var img = newCountry.AddComponent<Image>();
            //img.sprite = svg.sprite;
            //img.alphaHitTestMinimumThreshold = 0.1f;

            // AnchorPresets을 bottom, left로 설정
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);

            // Pivot 0, 0으로 설정
            rect.pivot = new Vector2(0, 0);

            // Scale 설정
            rect.localScale = new Vector3(700f, 700f, 1f);

            // location을 x, y좌표 설정
            rect.localPosition = c.getLocation();

            // worldmap_svg (0번 자식) 밑으로 위치
            newCountry.transform.SetParent(this.transform.GetChild(0));

            svg.sortingLayerName = "Background";
        }

        // 비율 조정
        // CountryCoordinateCalculator calc = new CountryCoordinateCalculator(countryList, 0.54f);
        GameObject.Find("Main Camera").AddComponent<CountryCoordinateCalculator>();

        // 버튼 생성
        // CountryButtonAdapterController controller = new CountryButtonAdapterController();
        GameObject.Find("Main Camera").AddComponent<CountryButtonAdapterController>();

        // 렌더 모드를 바꿔서 제대로 보여지도록
        GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
    }
}
