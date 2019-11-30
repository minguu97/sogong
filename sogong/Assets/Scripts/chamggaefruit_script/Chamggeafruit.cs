//#define BUILD
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.UI;
public class Chamggeafruit : MonoBehaviour
{
    public Image fruit;
 
    public Text answerText;
    Sprite[] fruitImages;
    ArrayList folderNames;
    int imageIndex = 0;
    int folderIndex = 0;

    public Button answerImage;
    public Button nextImage;
    public Button back;
    public Button ending;

    // Start is called before the first frame update
    void Start()
    {
#if (BUILD)
        makeFolderListTxt();
#endif
        ending.gameObject.SetActive(false);
        folderNames = new ArrayList();
        LoadFolders();
        Nextfruit();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadFolders()
    {
        TextAsset data = Resources.Load("fruit/List", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        string line;
        line = sr.ReadLine();
        while (line != null)
        {
            folderNames.Add(line);
            line = sr.ReadLine();
        }
        Debug.Log(folderNames[0]);
    }

    private void LoadFruitImages(string folderName)
    {
        string path = "fruit/" + folderName;
        fruitImages = Resources.LoadAll<Sprite>(path);
    
    }

    public void Nextfruit()
    {
        if (folderNames.Count == folderIndex)
        {
            ending.gameObject.SetActive(true);
            fruit.gameObject.SetActive(false);
            answerImage.gameObject.SetActive(false);
            nextImage.gameObject.SetActive(false);
            back.gameObject.SetActive(false);
            answerText.gameObject.SetActive(false); 
        }
        else
        {
            LoadFruitImages((string)folderNames[folderIndex]);
            if(imageIndex >= 2)
            {
                imageIndex = 0;
            }
            fruit.GetComponent<Image>().sprite = fruitImages[imageIndex];
        }
    }

    public void onclickanswerImage()
    {
        imageIndex++;
        answerText.gameObject.SetActive(true);
        answerText.GetComponent<Text>().text =
            Regex.Replace((string)folderNames[folderIndex], "[0-9]", "");
        Nextfruit();
    }

    public void onclicknextImage()
    {
        folderIndex++;
        imageIndex = 0;
        answerText.gameObject.SetActive(false);
        Nextfruit();
    }

    public void onclickback()
    {
        SceneManager.LoadScene("종류");
    }

    public void onclickending()
    {
        SceneManager.LoadScene("종류");
    }
#if (BUILD)
    //동물 폴더 리스트 TXT 생성 (빌드때만실행)
    public void makeFolderListTxt() {
        //존재하는 List 파일 삭제
        System.IO.FileInfo fi = new System.IO.FileInfo("Assets/Resources/Fruit/List.txt");
        try {
            fi.Delete();
        }
        catch (System.IO.IOException e) {
            Console.WriteLine(e.Message);
        }

        //새로 List 파일 생성
        string savePath = "Assets/Resources/Fruit/List.txt";
        string textValue;
        string animalFolderPath = "Assets/Resources/Fruit";

        DirectoryInfo di = new DirectoryInfo(animalFolderPath);
        foreach (DirectoryInfo directory in di.GetDirectories()) {
            textValue = directory.Name;
            System.IO.File.AppendAllText(savePath, textValue + "\n");
        } 
    }
#endif
}
