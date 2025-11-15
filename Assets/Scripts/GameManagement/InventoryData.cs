using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData{
    public List<string> itens;
    //public List<int> quests;

    public InventoryData(List<string> itens) {
        this.itens = itens;
    }
}
