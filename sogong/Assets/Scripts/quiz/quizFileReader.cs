using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class quizFileReader
{

    public List<quizNation> readAll()
    {
        List<quizNation> countryList = new List<quizNation>();

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

            quizNation quiznation = new quizNation(values[0],
                   values[1]);
            countryList.Add(quiznation);
        }

        Debug.Log("File Read Complete...");

        return countryList;
    }
}
