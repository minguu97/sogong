using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back_bt : MonoBehaviour {
    public void ChangeMenu() {
        SceneManager.LoadScene("종류");
    }
}
