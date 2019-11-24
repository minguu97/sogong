using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country
{
    string name;
    Vector2 location;

    public Country(string n, float x, float y)
    {
        name = n;
        location = new Vector2(x, y);
    }

    public Vector2 getLocation()
    {
        return location;
    }

    public string getName()
    {
        return name;
    }
}
