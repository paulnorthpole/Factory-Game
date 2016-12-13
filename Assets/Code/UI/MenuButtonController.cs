using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour {
    public void OnClick(string action) {
        if(action == "Play") {
            SceneManager.LoadScene("LoadingScene/Loading");
        } else {
            Application.Quit();
        }
    }
}
