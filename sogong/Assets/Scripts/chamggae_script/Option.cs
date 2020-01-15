using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    GameObject animalButtonPrefab;
    public GameObject content;
    public GameObject option;
    public GameObject chamgameScript;
    public Toggle silhouetteTogle;
    bool isEnable = false;

    // Start is called before the first frame update
    void Start()
    {
        animalButtonPrefab = Resources.Load<GameObject>("Prefabs/AnimalList_prefab");
        DisableOption();
        CreateListButtons();

        if (animalButtonPrefab == null)
        {
            Debug.Log("Prefab load fail.. animalButtonPrefab is NULL");
        }
    }

    public void CreateListButtons()
    {
        ArrayList list =
            GameObject.Find("gameScript").GetComponent<ChamGameVer2>().GetFolderNames();

        for (int i = 0; i < list.Count; i++)
        {
            GameObject button = Instantiate(animalButtonPrefab);
            button.transform.SetParent(content.transform);
            GameObject btText = button.transform.GetChild(0).gameObject;
            btText.GetComponent<Text>().text = (string)list[i];
        }
    }

    public void OptionBtnClick()
    {
        if (isEnable == false)
        {
            EnableOption();
        }
        else
        {
            DisableOption();
        }
    }

    private void EnableOption()
    {
        option.GetComponent<Canvas>().enabled = true;
        isEnable = true;
        chamgameScript.GetComponent<ChamGameVer2>().DisableButtons();
    }

    private void DisableOption()
    {
        option.GetComponent<Canvas>().enabled = false;
        isEnable = false;
        chamgameScript.GetComponent<ChamGameVer2>().EnableButtons();
    }

    public void SetSilhouette()
    {
        if (silhouetteTogle.isOn)
        {
            GameObject.Find("UI").transform.Find("Silhouette").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("Silhouette").SetActive(false);
        }
    }
}
