using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour{
    public static PauseMenuManager instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        gameObject.SetActive(false);
    }


    public void OpenMenu() {
        InputMapManager.instance.EnableMap("PauseMenu");
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
    }

    public void OnCloseMenu(InputAction.CallbackContext context) {
        if (context.performed) {
            InputMapManager.instance.EnableMap("Gameplay");
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnSave() {
        SaveManager.instance.SaveGame();
    }

    public void OnMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
