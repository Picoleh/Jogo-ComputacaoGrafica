using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnNewGame() {
        SaveManager.instance.ChangeScenes(true);
    }

    public void OnLoadGame() {
        SaveManager.instance.ChangeScenes(false);
    }

    public void OnExit() {
        Application.Quit();
    }
}
