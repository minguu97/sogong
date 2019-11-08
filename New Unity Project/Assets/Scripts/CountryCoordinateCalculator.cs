using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryCoordinateCalculator : MonoBehaviour
{
    float K = (float)1.29;

    string[] country = { "china", "russia", "mongolia", "vietnam", "thailand", "laos", "cambodia" };

    RectTransform pivot;

    // Start is called before the first frame update
    void Start()
    {
        foreach(string countryName in country)
        {
            RectTransform childRect = transform.Find(countryName).GetComponent<RectTransform>();
            childRect.anchoredPosition = new Vector3(childRect.anchoredPosition.x / K, childRect.anchoredPosition.y / K, 0);

        }
    }
}
