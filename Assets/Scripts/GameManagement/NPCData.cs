using UnityEngine;

[System.Serializable]
public class NPCData {
    public bool firstInteraction;
    public bool questCompleted;

    public NPCData(bool firstInteraction, bool questCompleted) { 
        this.firstInteraction = firstInteraction;
        this.questCompleted = questCompleted;
    }
}