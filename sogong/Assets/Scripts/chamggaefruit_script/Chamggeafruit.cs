using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;
public class Chamggeafruit : MonoBehaviour
{
    public Image fruit;
    public Image ending;
    Sprite[] fruitImages;
    ArrayList folderNames;
    int imageIndex = 0;
    int folderIndex = 0;

    public Button answerImage;
    public Button nextImage;

    // Start is called before the first frame update
    void Start()
    {
        folderNames = new ArrayList();
        LoadFolders();
        Nextfruit();
        ending.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadFolders()
    {
        string Path = "Assets/Resources/fruit";

        DirectoryInfo dict = new DirectoryInfo(Path);
        foreach (DirectoryInfo directory in dict.GetDirectories())
        {
            folderNames.Add(directory.Name);
            Debug.Log(directory.Name);
        }
    }

    private void LoadAnimalImages(string folderName)
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
        }
        else
        {
            LoadAnimalImages((string)folderNames[folderIndex]);
            fruit.GetComponent<Image>().sprite = fruitImages[imageIndex];
        }
    }

    public void onclickanswerImage()
    {
        imageIndex++;
        Nextfruit();
    }

    public void onclicknextImage()
    {
        folderIndex++;
        imageIndex = 0;
        Nextfruit();
    }
}
