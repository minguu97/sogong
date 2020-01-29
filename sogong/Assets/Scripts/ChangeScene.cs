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
                SceneManager.LoadScene("World map(upgrade)");
                break;
            case 3:
                SceneManager.LoadScene("참깨-과일");
                break;
            case 4:
                SceneManager.LoadScene("참깨-동물");
                break;
            case 5:
                SceneManager.LoadScene("World Quiz");
                break;
            case 6:
                Application.Quit();
                break;
        }
	
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void unSelectButton() {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
