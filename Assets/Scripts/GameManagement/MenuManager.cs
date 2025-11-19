using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public enum MenuType {
    Main,
    Pause,
    Settings
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

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
        gameObject.SetActive(true);
    }

    public void CloseMenu() {
        InputMapManager.instance.EnableMap("Gameplay");
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
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
