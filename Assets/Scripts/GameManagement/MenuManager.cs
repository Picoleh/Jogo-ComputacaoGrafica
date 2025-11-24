using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum MenuType {
    Main,
    Pause,
    Settings,
    Credits,
    GameOver
}

public class MenuManager : MonoBehaviour, ISaveable
{
    public static MenuManager instance;

    [SerializeField] BatteryManager batteryManager;

    [SerializeField] Image bgImage;

    [Header("Mesma ordem do enum")]
    [SerializeField] private List<MenuBase> menus;



    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        OpenMenu(MenuType.Main);
        SaveManager.instance.LoadConfigData();
    }

    public void OpenMenu(MenuType menuType) {
        InputMapManager.instance.EnableMap("Menu");
        Cursor.lockState = CursorLockMode.None;
        foreach (var menu in menus) {
            menu.CloseMenu();
        }

        menus[(int)menuType].OpenMenu();
        bgImage.sprite = menus[(int)menuType].GetImage();
        StopBatteryConsume();
        gameObject.SetActive(true);
    }

    public void OpenMenu(MenuType menuType, GameOverType type) {
        InputMapManager.instance.DisableControls();
        Cursor.lockState = CursorLockMode.None;
        foreach (var menu in menus) {
            menu.CloseMenu();
        }

        menus[(int)menuType].OpenMenu(type, batteryManager.currentBattery, batteryManager.maxBattery);
        bgImage.sprite = menus[(int)menuType].GetImage();
        StopBatteryConsume();
        gameObject.SetActive(true);
    }

    public void CloseMenu() {
        if (SceneManager.GetActiveScene().name == "Game") {
            InputMapManager.instance.EnableMap("Gameplay");
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);
            ResumeBatteryConsume();
        }
    }

    public void StopBatteryConsume() {
        batteryManager.SetUsing(false);
    }

    public void ResumeBatteryConsume() {
        batteryManager.SetUsing(true);
    }

    public void GoBack() {
        if(SceneManager.GetActiveScene().name == "Game") { // Deve voltar ao pauseMenu
            OpenMenu(MenuType.Pause);
        }
        else { // Deve voltar ao MainMenu
            OpenMenu(MenuType.Main);
        }
    }

    public object GetData() {
        return new MenuData(Mathf.Round(batteryManager.currentBattery));
    }

    public void SetData(object data) {
        MenuData menuData = (MenuData)data;
        batteryManager.currentBattery = menuData.batteryLevel;
    }

    public IEnumerator LoadSceneRoutine(string sceneName, UnityAction<Scene, LoadSceneMode> onLoaded) {
        LoadingScript.instance.ShowLoadScreen();
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f) {
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        async.allowSceneActivation = true;

        SceneManager.sceneLoaded += onLoaded;
    }
}
