using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryCoordinateCalculator
{
    public CountryCoordinateCalculator(List<Country> countryList, float K)
    {
        foreach (Country country in countryList)
        {
            string countryName = country.getName();
            RectTransform childRect;
            GameObject worldmap_svg = GameObject.Find("worldmap_svg");
            if (worldmap_svg)
            {
                childRect = worldmap_svg.transform.Find(countryName).GetComponent<RectTransform>();
            } else
            {
                Debug.Log("'worldmap_svg'을 찾을 수 없습니다.");
                continue;
            }
            childRect.anchoredPosition = new Vector3(childRect.anchoredPosition.x / K, childRect.anchoredPosition.y / K, 0);
        }
    }
}
