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
            answerImage.gameObject.SetActive(false);
            nextImage.gameObject.SetActive(false);
            back.gameObject.SetActive(false);
            answerText.gameObject.SetActive(false); 
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
}
