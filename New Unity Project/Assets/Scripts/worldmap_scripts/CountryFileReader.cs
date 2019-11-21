using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CountryFileReader
{
    List<Country> countryList;
    static CountryFileReader countryFileReader;

    public CountryFileReader()
    {
        countryList = new List<Country>();
        Parse();
    }

    private void Parse()
    {
        string conutryFilePath = Application.dataPath + "/Resources/location.txt";

        FileStream fs = new FileStream(conutryFilePath, FileMode.Open);
        StreamReader sr = new StreamReader(fs);


        string readStr;
        while ((readStr = sr.ReadLine()) != null)
        {
            string[] values;
            values = readStr.Split(' ');
            if (values.Length == 0)
            {
                break;
            }

            Country country = new Country(values[0], 
                Convert.ToSingle(values[1]), 
                Convert.ToSingle(values[2]));

            countryList.Add(country);
        }

        Debug.Log("File Read Complete...");
        sr.Close();
        fs.Close();
    }

    public List<Country> GetList()
    {
        if (countryList.Count == 0)
        {
            Debug.Log("getList() error");
        }

        return countryList;
    }

    public static CountryFileReader GetInstance()
    {
        if (countryFileReader == null)
        {
            countryFileReader = new CountryFileReader();
        }
        return countryFileReader;
    }
}
