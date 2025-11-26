using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SaveManager : MonoBehaviour{
    public static SaveManager instance;
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

        MenuData menuData = MenuManager.instance.GetData() as MenuData;

        SaveSystem.Save(new GameData(playerData, inventoryData, nPCDatas, menuData));
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

        MenuManager.instance.SetData(gameData.menuData);
    }

    public void LoadConfigData() {
        ConfigData configData = SaveSystem.LoadConfigSave();
        if (configData == null) 
            return;

        settingsMenu.SetData(configData);
    }
}
