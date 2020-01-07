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

    }

    void Update()
    {
        
    }
    public void Select(int s)
    {
        switch (s)
        {
            case 0:
                SceneManager.LoadScene("시작");
                break;
            case 1:
                SceneManager.LoadScene("종류");
                break;
            case 2:
                SceneManager.LoadScene("지도");
                break;
            case 3:
                SceneManager.LoadScene("참깨-과일");
                break;
            case 4:
                SceneManager.LoadScene("chamAnimal");
                break;
            case 5:
                Application.Quit();
                break;
        }
	
    }

    public void Select2()
    {
	
    }
    public void Select3()
    {
        

    }
    public void Select4()
    {
        
    }
    public void Select5()
    {
        
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void unSelectButton() {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
