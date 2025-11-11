using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData{
    public PlayerData playerData;
    public InventoryData inventoryData;
    public List<NPCData> npcData;

    public GameData(PlayerData playerData, InventoryData inventoryData, List<NPCData> npcData) {
        this.playerData = playerData;
        this.inventoryData = inventoryData;
        this.npcData = npcData;
    }
}
