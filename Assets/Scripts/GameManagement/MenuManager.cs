using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public enum MenuType {
    Main,
    Pause,
    Settings,
    GameOver
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] BatteryManager batteryManager;

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
        batteryManager.SetUsing(false);
        gameObject.SetActive(true);
    }

    public void OpenMenu(MenuType menuType, GameOverType type) {
        InputMapManager.instance.DisableControls();
        Cursor.lockState = CursorLockMode.None;
        foreach (var menu in menus) {
            menu.CloseMenu();
        }

        menus[(int)menuType].OpenMenu(type, batteryManager.currentBattery, batteryManager.maxBattery);
        batteryManager.SetUsing(false);
        gameObject.SetActive(true);
    }

    public void CloseMenu() {
        InputMapManager.instance.EnableMap("Gameplay");
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
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

}
