using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "Scriptable Objects/QuesInfo")]
public class QuestInfo : ScriptableObject{
    public int id;
    private string _ownerName;
    public string questTitle;
    private bool completed;
    [TextArea(5, 10)]
    public string description;

    public Dialogue questDialogue;

    public Dialogue getCombackDialog() {
        if (!completed) {
            completed = true;
            return comebackInProgress;
        }
        else {
            return comebackFinished;
        }
    }

    public bool isCompleted() {
        return completed;
    }

    public ItemInfo getReward() {
        return itemReward;
    }

    public void setOwnerName(string ownerName) { 
        _ownerName = ownerName;
    }

    public string getOwnerName() { 
        return _ownerName;
    }

    [Header("Options")]
    public Dialogue comebackInProgress;
    public Dialogue comebackFinished;

    [Header("Rewards")]
    public ItemInfo itemReward;
}
