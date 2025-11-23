using UnityEngine;
using UnityEngine.UI;

public class MenuBase : MonoBehaviour
{
    [SerializeField] Sprite bg;

    public virtual void OpenMenu() {
        gameObject.SetActive(true);
    }

    public virtual void OpenMenu(GameOverType type, float battery, float maxBattery) {
    }

    public virtual void CloseMenu() {
        gameObject.SetActive(false);
    }

    public virtual Sprite GetImage() {
        return bg;
    }
}
