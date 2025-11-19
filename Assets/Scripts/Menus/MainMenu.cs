using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnNewGame() {
        SaveManager.instance.ChangeScenes(newSave:true);
        gameObject.SetActive(false);
    }

    public void OnLoadGame() {
        SaveManager.instance.ChangeScenes(newSave:false);
        gameObject.SetActive(false);
    }

    public void OnExit() {
        Application.Quit();
    }
}
