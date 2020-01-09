using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileReader
{
    public List<Nation> parse()
    {
        List<Nation> countryList = new List<Nation>() ;

        string readStr;

        // 나라이름 한국말 파일 load
        Debug.Log("'koreanCN.txt' file Read Start...");
        TextAsset data_korean = Resources.Load("koreanCN", typeof(TextAsset)) as TextAsset;
        StringReader sr_k = new StringReader(data_korean.text);

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

        // 전세계 나라 정보 파일 load
        Debug.Log("'location.txt' file Read Start...");
        TextAsset data = Resources.Load("location", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        while ((readStr = sr.ReadLine()) != null)
        {
            string[] values;
            values = readStr.Split(' ');
            if (values.Length == 0)
            {
                break;
            }

            // 각 나라의 한국 이름 찾아서 Country 객체 만들어
            if (koreanCN.ContainsKey(values[0]))
            {
                Nation nation = new Nation(values[0],
                   Convert.ToSingle(values[1]),
                   Convert.ToSingle(values[2]),
                   koreanCN[values[0]]);

                countryList.Add(nation);
            }
        }

        Debug.Log("File Read Complete...");
        sr.Close();

        return countryList;
    }
}
