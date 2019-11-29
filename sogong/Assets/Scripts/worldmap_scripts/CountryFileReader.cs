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
        Debug.Log("File Read Start...");

        string readStr;

        TextAsset data = Resources.Load("location", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        TextAsset data_korean = Resources.Load("koreanCN", typeof(TextAsset)) as TextAsset;
        StringReader sr_k = new StringReader(data_korean.text);

        // 나라이름 한국말 파일 load
        Dictionary<string, string> koreanCN = new Dictionary<string, string>();
        while ((readStr = sr_k.ReadLine()) != null)
        {
            string[] values;
            values = readStr.Split(' ');
            if (readStr.Length == 0)
            {
                break;
            }

            koreanCN.Add(values[0], values[1]);
            // Debug.Log("TEST: " + values[0] + " / " + values[1]);
        }

        while ((readStr = sr.ReadLine()) != null)
        {
            string[] values;
            values = readStr.Split(' ');
            if (values.Length == 0)
            {
                break;
            }

            if (koreanCN.ContainsKey(values[0]))
            {
                Country country = new Country(values[0],
                   Convert.ToSingle(values[1]),
                   Convert.ToSingle(values[2]),
                   koreanCN[values[0]]);

                countryList.Add(country);
            }
        }

        Debug.Log("File Read Complete...");
        sr.Close();
        // fs.Close();
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
