using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    public static LoadingScript instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        HideLoadScreen();
        DontDestroyOnLoad(gameObject);
    }

    public void ShowLoadScreen() {
        gameObject.SetActive(true);
    }

    public void HideLoadScreen() { 
        gameObject.SetActive(false);
    }
}
