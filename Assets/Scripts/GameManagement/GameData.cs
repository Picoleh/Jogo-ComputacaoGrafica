using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData{
    public PlayerData playerData;
    public InventoryData inventoryData;
    public List<NPCData> npcData;
    public MenuData menuData;

    public GameData(PlayerData playerData, InventoryData inventoryData, List<NPCData> npcData, MenuData menuData) {
        this.playerData = playerData;
        this.inventoryData = inventoryData;
        this.npcData = npcData;
        this.menuData = menuData;
    }
}
