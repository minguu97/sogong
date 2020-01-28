using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Option : MonoBehaviour
{
    GameObject animalButtonPrefab;
    public GameObject content;
    public GameObject option;
    public GameObject chamgameScript;
    public Toggle silhouetteTogle;
    public string CurrentAnimalBtnText;
    bool isEnable = false;

    // Start is called before the first frame update
    void Start()
    {
        animalButtonPrefab = Resources.Load<GameObject>("Prefabs/AnimalList_prefab");
        DisableOption();
        UpdateListButtons();

        if (animalButtonPrefab == null)
        {
            Debug.Log("Prefab load fail.. animalButtonPrefab is NULL");
        }
    }

    public void UpdateImage()
    {
        GameObject.Find("gameScript").GetComponent<ChamGameVer2>().UpdateImage();
    }

    public void UpdateListButtons()
    {
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        ArrayList list =
            GameObject.Find("gameScript").GetComponent<ChamGameVer2>().GetFolderNames();

        for (int i = 0; i < list.Count; i++)
        {
            GameObject button = Instantiate(animalButtonPrefab);
            button.transform.SetParent(content.transform);
            GameObject btText = button.transform.GetChild(0).gameObject;
            btText.GetComponent<Text>().text = (string)list[i];
        }

        AddButtonEventListener();
    }

    public void ShuffleList()
    {
        GameObject.Find("gameScript").GetComponent<ChamGameVer2>().ShuffleList();
        UpdateListButtons();
        UpdateImage();
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
        UpdateListButtons();
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

    public void AddButtonEventListener()
    {
        // Add event listeners to all buttons in the canvas
        Button[] buttons = content.gameObject.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            string identifier = buttons[i].name;
            buttons[i].onClick.AddListener(delegate { OnButtonClicked(identifier); });
        }
    }

    public void OnButtonClicked(string identifier)
    {
        GameObject btn = EventSystem.current.currentSelectedGameObject;
        string name = btn.transform.GetChild(0).gameObject.GetComponent<Text>().text;
        GameObject.Find("gameScript").GetComponent<ChamGameVer2>().OnButtonClicked(name);
    }
}
