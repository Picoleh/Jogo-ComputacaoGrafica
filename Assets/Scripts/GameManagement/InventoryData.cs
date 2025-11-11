using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData{
    public List<string> itens;
    public List<int> quests;

    public InventoryData(List<string> itens, List<int> quests) {
        this.itens = itens;
        this.quests = quests;
    }
}
