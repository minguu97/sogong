using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country
{
    string name;
    Vector2 location;
    string koreanName;
    CountryButtonAdapter adapter;

    public Country(string n, float x, float y, string ko)
    {
        name = n;
        location = new Vector2(x, y);
        koreanName = ko;
    }

    public Vector2 getLocation()
    {
        return location;
    }

    public string getName()
    {
        return name;
    }

    public string getKoreanName()
    {
        return koreanName;
    }

    public CountryButtonAdapter getAdapter()
    {
        return adapter;
    }

    public void setAdapter(CountryButtonAdapter adapter)
    {
        this.adapter = adapter; 
    }
}
