using UnityEngine;

public class MenuBase : MonoBehaviour
{
    public void OpenMenu() {
        gameObject.SetActive(true);
    }

    public void CloseMenu() {
        gameObject.SetActive(false);
    }
}
