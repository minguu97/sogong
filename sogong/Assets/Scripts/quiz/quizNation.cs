using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quizNation
{
    string name;
    string koreaname;

    public quizNation(string n ,string ko)
    {
        name = n;

        koreaname = ko;
    }

    public string getName()
    {
        return name;
    }

    public string getKN()
    {
        return koreaname;
    }
}
