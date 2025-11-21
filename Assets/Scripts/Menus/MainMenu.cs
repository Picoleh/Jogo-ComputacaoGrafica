using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MenuBase
{
    [SerializeField] Button settingsButton;
    [SerializeField] BatteryManager batteryManager;

    private void Awake() {
        settingsButton.onClick.AddListener(OnSettingsClick);
    }

    public void OnNewGame() {
        MenuManager.instance.CloseMenu();
        SaveManager.instance.ClearRegisters();
        SaveManager.instance.ChangeScenes(newSave:true);
    }

    public void OnLoadGame() {
        MenuManager.instance.CloseMenu();
        SaveManager.instance.ClearRegisters();
        SaveManager.instance.ChangeScenes(newSave:false);
    }

    private void OnSettingsClick() {
        MenuManager.instance.OpenMenu(MenuType.Settings);
    }

    public void OnExit() {
        Application.Quit();
    }

    public override void OpenMenu() {
        base.OpenMenu();
        batteryManager.Reset();
    }
}
