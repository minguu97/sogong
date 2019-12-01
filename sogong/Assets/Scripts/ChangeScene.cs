using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour{
    void fix()
    {
	
    }
    void Start()
    {
        Screen.SetResolution(1024, 768, true);
    }

    void Update()
    {
        
    }
    public void Select1()
    {
	SceneManager.LoadScene("종류");
    }

    public void Select2()
    {
	SceneManager.LoadScene("지도");
    }
    public void Select3()
    {
        SceneManager.LoadScene("참깨-과일");

    }
    public void Select4()
    {
        SceneManager.LoadScene("chamAnimal");
    }
    public void Select5()
    {
        SceneManager.LoadScene("시작");
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void unSelectButton() {
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void Quit()
    {
	Application.Quit();
    }
}
