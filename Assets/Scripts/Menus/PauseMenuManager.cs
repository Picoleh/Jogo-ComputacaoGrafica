using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MenuBase
{
    public static PauseMenuManager instance;
    [SerializeField] Button settingsButton;
    [SerializeField] Button mainMenuButton;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        settingsButton.onClick.AddListener(OnSettingsClick);
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    public void OnCloseMenu(InputAction.CallbackContext context) {
        if (context.performed) {
            
        }
    }

    private void OnSettingsClick() {
        MenuManager.instance.OpenMenu(MenuType.Settings);
    }

    private void OnMainMenuClick() {
        SceneManager.LoadScene("MainMenu");
        MenuManager.instance.OpenMenu(MenuType.Main);
    }

    public void OnSave() {
        SaveManager.instance.SaveGame();
    }

    public void OnMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
