using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Nation
{
    string name;
    Vector2 location;
    string koreanName;

    public Nation(string n, float x, float y, string ko)
    {
        name = n;
        location = new Vector2(x, y);
        koreanName = ko;
    }

    public string getName()
    {
        return name;
    }

    public Vector2 getLocation()
    {
        return location;
    }

    public string getKoreanName()
    {
        return koreanName;
    }
}
