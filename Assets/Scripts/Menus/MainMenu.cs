using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MenuBase
{
    [SerializeField] Button settingsButton;
    [SerializeField] Button loadGameButton;
    [SerializeField] Button creditsButton;
    [SerializeField] BatteryManager batteryManager;

    private bool loadSave = false;

    private void Awake() {
        settingsButton.onClick.AddListener(OnSettingsClick);
        creditsButton.onClick.AddListener(OnCreditsClick);
        loadGameButton.interactable = false;
    }

    public void OnNewGame() {
        SaveManager.instance.ClearRegisters();
        //SaveManager.instance.ChangeScenes(newSave:true);
        loadSave = false;
        StartCoroutine(MenuManager.instance.LoadSceneRoutine("Intro", OnIntroSceneLoaded));
    }

    public void OnLoadGame() {
        SaveManager.instance.ClearRegisters();
        loadSave = true;
        StartCoroutine(MenuManager.instance.LoadSceneRoutine("Game", OnGameSceneLoaded));
    }

    private void OnIntroSceneLoaded(Scene scene, LoadSceneMode mode) {
        SceneManager.sceneLoaded -= OnIntroSceneLoaded;
        LoadingScript.instance.HideLoadScreen();
        MenuManager.instance.CloseMenu();
    }

    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (loadSave) {
            SaveManager.instance.LoadGameData();
        }
        SceneManager.sceneLoaded -= OnGameSceneLoaded;
        InputMapManager.instance.GetInputReferences();
        LoadingScript.instance.HideLoadScreen();
        MenuManager.instance.CloseMenu();
    }

    private void OnSettingsClick() {
        MenuManager.instance.OpenMenu(MenuType.Settings);
    }

    private void OnCreditsClick() {
        MenuManager.instance.OpenMenu(MenuType.Credits);
    }

    public void OnExit() {
        Application.Quit();
    }

    public override void OpenMenu() {
        base.OpenMenu();
        batteryManager.Reset();
        if(SaveSystem.GameFileExists())
            loadGameButton.interactable = true;
    }
}
