using UnityEngine;

[System.Serializable]
public class NPCData {
    public bool firstInteraction;
    public bool receivedReward;

    public NPCData(bool firstInteraction, bool receivedReward) { 
        this.firstInteraction = firstInteraction;
        this.receivedReward = receivedReward;
    }
}