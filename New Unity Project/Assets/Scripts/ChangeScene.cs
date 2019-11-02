using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour{
    void fix()
    {
	Screen.SetResolution(1280, 768, true);
    }
    void Start()
    {
        
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

    public void Goback()
    {
	SceneManager.LoadScene("시작");
    }
    public void Quit()
    {
	Application.Quit();
    }
}
