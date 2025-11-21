using UnityEngine;

public class MenuBase : MonoBehaviour
{
    public virtual void OpenMenu() {
        gameObject.SetActive(true);
    }

    public virtual void OpenMenu(GameOverType type, float battery, float maxBattery) {
    }

    public virtual void CloseMenu() {
        gameObject.SetActive(false);
    }
}
