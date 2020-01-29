using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class quizmgr : MonoBehaviour
{
    public GameObject correcttext;
    public GameObject panel1;
    public GameObject panel2;
    public SVGImage imageo;
    public SVGImage image1;
    public SVGImage image2;
    List<quizNation> List;
    int correctnum = 0;

    int qeusetionnum1 = 0;
    int qeusetionnum2 = 0;

    void Start()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        correcttext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickQuiz()
    {
        quizFileReader reader = new quizFileReader();
        List<quizNation> List = reader.readAll();
        Text text = GameObject.Find("Findname").GetComponent<Text>();
        int count = 0;
        panel1.SetActive(false);
        panel2.SetActive(true);

        Sprite coerrectflag = null;
        Sprite wrongflag = null;
        Sprite wrongflag2 = null;

        // 정답인 국기 찾아서 할당하기
        foreach (quizNation n in List)
        {
            string comp = text.text;
            if (comp == n.getKN())
            {
                string path = "flags/" + n.getName();
                coerrectflag = Resources.Load<Sprite>(path);
            }
            count++;
        }
        pickquestionnation(count);
        count = 0;

        //오답인 국기 찾아서 할당하기
        foreach (quizNation n in List)
        {
            if(qeusetionnum1 == count)
            {
                string path = "flags/" + n.getName();
                wrongflag = Resources.Load<Sprite>(path);
            }

            if(qeusetionnum2 == count)
            {
                string path = "flags/" + n.getName();
                wrongflag2 = Resources.Load<Sprite>(path);
            }
            count++;
        }

        setexam(coerrectflag, wrongflag, wrongflag2);
    }
    // 오답의 나라 번호를 선택하는 함수
    public void pickquestionnation(int c)
    {

        while (true)
        {
            qeusetionnum1 = Random.Range(0, 201);
            if (c != qeusetionnum1)
                break;
        }
        
        while (true)
        {
            qeusetionnum2 = Random.Range(0, 201);
            if (qeusetionnum1 != qeusetionnum2)
                break;
        }

    }

    public void setexam(Sprite coerrectflag , Sprite wrongflag , Sprite wrongflag2)
    {
        Sprite[] flags = { coerrectflag, wrongflag, wrongflag2 };
        flags = suffle(flags);
        imageo.GetComponent<SVGImage>().sprite = flags[0];
        image1.GetComponent<SVGImage>().sprite = flags[1];
        image2.GetComponent<SVGImage>().sprite = flags[2];
    }

    public Sprite[] suffle(Sprite[] flags)
    {
        int rn;
        Sprite temp;
        for (int i = 0; i < 3; i++)
        {
            rn = Random.Range(0, 2);
            temp = flags[i];
            flags[i] = flags[rn];
            flags[rn] = temp;
            if (correctnum == i)
                correctnum = rn;
            else if (correctnum == rn)
                correctnum = i;
        }
        Debug.Log(correctnum);
        return flags;
    }

    public void comfirmcorret1()
    {
        if (correctnum == 0)
        {
            panel2.SetActive(false);
            correcttext.SetActive(true);
        }

    }
    public void comfirmcorret2()
    {

        if (correctnum == 1)
        {
            panel2.SetActive(false);
            correcttext.SetActive(true);
        }
    }
    public void comfirmcorret3()
    {
        if (correctnum == 2)
        {
            panel2.SetActive(false);
            correcttext.SetActive(true);
      
        }
    }

    public void backclick()
    {
        SceneManager.LoadScene("World map(upgrade)");
    }
}
