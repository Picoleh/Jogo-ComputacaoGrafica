
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest{
    private bool completed;
    [SerializeField] private QuestInfo info;
    [SerializeField] private string _questTitle;

    public Dialogue getQuestDialog() {
        return info.questDialogue;
    }

    public Dialogue getCombackDialog() {
        if (!completed) {
            completed = true;
            return info.comebackInProgress;
        }
        else {
            return info.comebackFinished;
        }
    }

    public bool isCompleted() {
        return completed;
    }

    public ItemInfo getReward() {
        return info.itemReward;
    }

    public string getQuestTitle() {
        return _questTitle;
    }

    public string getQuestDescription() {
        return info.description;
    }

    public int getQuestId() {
        return info.id;
    }
}
