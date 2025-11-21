using UnityEngine;

public abstract class QuestBase : ScriptableObject{
    private string _ownerName;
    public bool _completed;

    // Info
    public int id;
    public string questTitle;
    [TextArea(5, 10)]
    public string description;
    public Dialogue questDialogue;
    public Dialogue comebackInProgress;
    public Dialogue comebackFinished;

    // Reward
    public ItemInfo itemReward;

    // Metodos
    public Dialogue getCombackDialog() {
        if (!CheckComplete()) {
            return comebackInProgress;
        }
        else {
            return comebackFinished;
        }
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

    public void GiveReward() {
        if (itemReward != null) { 
            InventoryManager.instance.AddItem(itemReward);
            DialogueSystem.instance.OnDialogueEnd -= GiveReward;
        }
        else {
            itemReward = null;
        }

        if(_ownerName == "Tonclay")
            MenuManager.instance.OpenMenu(MenuType.GameOver, GameOverType.Won);
    }

    public void AddQuestToInventory() {  
        InventoryManager.instance.AddQuest(this);
        DialogueSystem.instance.OnDialogueEnd -= AddQuestToInventory;
    }

    public void ShowSucessNotification() {
        NotificationManager.instance.ShowNotification("Quest Completa");
        DialogueSystem.instance.OnDialogueEnd -= ShowSucessNotification;
    }

    // Abstract
    public abstract void StartQuest();

    public abstract bool CheckComplete();

    public abstract void RedoQuest();
    
}
