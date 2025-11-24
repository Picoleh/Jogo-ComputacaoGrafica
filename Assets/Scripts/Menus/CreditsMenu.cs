using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : MenuBase
{
    [SerializeField] Button mainMenuButton;

    private void Awake() {
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    private void OnMainMenuClick() {
        MenuManager.instance.OpenMenu(MenuType.Main);
    }
}
