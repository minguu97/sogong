//#define BUILD

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;

using Object = UnityEngine.Object;

public class ChamGameVer2 : MonoBehaviour
{
    GameObject animalObj;
    GameObject silhouette;
    GameObject answerText;
    public GameObject endImg;

    Sprite[] animalImages;
    ArrayList folderNames;

    int imageIndex = 1;
    int folderIndex = 0;

    //실루엣
    public void GetSilhouette() {
        silhouette.GetComponent<Image>().sprite = animalImages[0];
    }

    //동물 다음 부분 이미지 가져오기
    public void GetNextPartImage() {
        if (animalImages.Length > imageIndex) {
            animalObj.GetComponent<Image>().sprite = animalImages[imageIndex++];
        }
        else {
            animalObj.GetComponent<Image>().sprite = animalImages[0];
        }
    }

    //다음 동물로 넘어가기
    public void GetNextAnimal() {
        imageIndex = 1;
        if (folderNames.Count - 1 > folderIndex) {
            LoadAnimalImages((string)folderNames[++folderIndex]);
            answerText.GetComponent<Text>().text = "";
            animalObj.GetComponent<Image>().sprite =
                GameObject.Find("Empty").GetComponent<SpriteRenderer>().sprite;
            GetSilhouette();
        }
        else {
            endImg.gameObject.SetActive(true);
            animalObj.SetActive(false);
            silhouette.SetActive(false);
            answerText.SetActive(false);
            GameObject.Find("nextPart_bt").SetActive(false);
            GameObject.Find("nextImg_bt").SetActive(false);
            GameObject.Find("answer_bt").SetActive(false);
        }
    }

    //정답 보기
    public void Answer() {
        animalObj.GetComponent<Image>().sprite = animalImages[0];
        answerText.GetComponent<Text>().text = 
            Regex.Replace((string)folderNames[folderIndex], "[0-9]", "");
    }



    // Start is called before the first frame update
    void Start()
    {
#if (BUILD)
        makeFolderListTxt();
#endif
        animalObj = GameObject.Find("AnimalObj");
        silhouette = GameObject.Find("Silhouette");
        answerText = GameObject.Find("answer_txt");

        folderNames = new ArrayList();

        LoadFolders();
        folderNames = ShuffleArrayList(folderNames);

        GetNextAnimal();
        //Test
        Debug.Log("Array shuffle complete, First folder name is " + folderNames[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //배열 셔플
    private ArrayList ShuffleArrayList(ArrayList source) {
        ArrayList sortedList = new ArrayList();
        System.Random generator = new System.Random();

        while (source.Count > 0) {
            int position = generator.Next(source.Count);
            sortedList.Add(source[position]);
            source.RemoveAt(position);
        }

        return sortedList;
    }

    //동물 이미지 배열에 로딩
    private void LoadAnimalImages(string folderName) {
        string path = "Animal/" + folderName;

        animalImages = Resources.LoadAll<Sprite>(path);

        //Test
        Debug.Log("Image loading complete " + path);
    }


    //동물 폴더 리스트 로딩
    private void LoadFolders() {
        TextAsset data = Resources.Load("Animal/List", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        string line;
        line = sr.ReadLine();
        while(line != null) {
            folderNames.Add(line);
            line = sr.ReadLine();
        }

        //Test
        Debug.Log("Folder loading complete, First folder name is " + folderNames[0]);
    }

#if (BUILD)
    //동물 폴더 리스트 TXT 생성 (빌드때만실행)
    public void makeFolderListTxt() {
        //존재하는 List 파일 삭제
        System.IO.FileInfo fi = new System.IO.FileInfo("Assets/Resources/Animal/List.txt");
        try {
            fi.Delete();
        }
        catch (System.IO.IOException e) {
            Console.WriteLine(e.Message);
        }

        //새로 List 파일 생성
        string savePath = "Assets/Resources/Animal/List.txt";
        string textValue;
        string animalFolderPath = "Assets/Resources/Animal";

        DirectoryInfo di = new DirectoryInfo(animalFolderPath);
        foreach (DirectoryInfo directory in di.GetDirectories()) {
            textValue = directory.Name;
            System.IO.File.AppendAllText(savePath, textValue + "\n");
        } 
    }
#endif
}
