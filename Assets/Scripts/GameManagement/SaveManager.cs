using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SaveManager : MonoBehaviour{
    public static SaveManager instance;

    private bool loadSave;
    private PlayerMovement player;
    private InventoryManager inventory;
    [SerializeField] private SettingsMenu settingsMenu;
    private List<NPC> npcs = new();

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        loadSave = false;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterPlayer(PlayerMovement p) {
        player = p;
    }

    public void RegisterInventory(InventoryManager i) {
        inventory = i;
    }

    public void RegisterNPC(NPC n) {
        npcs.Add(n);
    }

    public void ClearRegisters() {
        player = null;
        inventory = null;
        npcs.Clear();
    }

    public void SaveGame() {
        PlayerData playerData = player.GetData() as PlayerData;
        InventoryData inventoryData = inventory.GetData() as InventoryData;

        List<NPCData> nPCDatas = new List<NPCData>();
        foreach (NPC n in npcs) { 
            nPCDatas.Add(n.GetData() as NPCData);
        }

        SaveSystem.Save(new GameData(playerData, inventoryData, nPCDatas));
    }

    public void SaveConfig() {
        SaveSystem.Save(settingsMenu.GetData() as ConfigData);
    }

    public void LoadGameData() {
        GameData gameData = SaveSystem.LoadGameSave();
        if (gameData == null)
            return;
        player.SetData(gameData.playerData);
        inventory.SetData(gameData.inventoryData);
        for (int i = 0; i < npcs.Count; i++) {
            npcs[i].SetData(gameData.npcData[i]);
        }
    }

    public void LoadConfigData() {
        ConfigData configData = SaveSystem.LoadConfigSave();
        if (configData == null) 
            return;

        settingsMenu.SetData(configData);
    }

    public void ChangeScenes(bool newSave) {
        loadSave = !newSave;
        StartCoroutine(LoadGameRoutine());
    }

    private IEnumerator LoadGameRoutine() {
        // 1️ Mostrar tela de loading
        LoadingScript.instance.ShowLoadScreen();

        // 2 Começar carregamento da cena
        AsyncOperation async = SceneManager.LoadSceneAsync("Game");
        async.allowSceneActivation = false;

        // 3️ Atualizar barra de progresso
        //while (async.progress < 0.9f) {
        //    if (progressBar != null)
        //        progressBar.value = async.progress / 0.9f;
        //    yield return null;
        //}

        while (async.progress < 0.9f) {
            yield return null;
        }
        
        // 4️(opcional) tempo mínimo de loading
        yield return new WaitForSeconds(1f);


        // 6️ Ativar a nova cena
        async.allowSceneActivation = true;

        SceneManager.sceneLoaded += OnSceneLoaded;

        // 5️ Restaurar dados (antes de ativar a cena)

        // 7️ Esconder tela de loading
        //if (loadingScreen != null)
        //loadingScreen.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (loadSave) {
            LoadGameData();
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
        InputMapManager.instance.GetInputReferences();
        LoadingScript.instance.HideLoadScreen();
    }
}
