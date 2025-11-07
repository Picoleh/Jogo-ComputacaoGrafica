using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable{
    [SerializeField] private string _npcName;
    [SerializeField] private string _prompt;
    [SerializeField] private Quest _quest;
    [SerializeField] private Animator _animator; 
    private bool _firstInteraction = true;
    private bool _receivedReward = false;

    public string interactionPrompt => _prompt;

    public void Interact(Interactor interactor) {
        _animator.SetTrigger("Interacted");
        //_animator.ResetTrigger("Interacted");
        if (_firstInteraction) { 
            DialogueSystem.instance.StartDialogue(_npcName, _quest.getQuestDialog());
            _firstInteraction = false;
            DialogueSystem.instance.OnDialogueEnd += ShowMissionNotification;
        }
        else {
            DialogueSystem.instance.StartDialogue(_npcName, _quest.getCombackDialog());
            if (_quest.isCompleted() && !_receivedReward) {
                DialogueSystem.instance.OnDialogueEnd += GiveReward;
                _receivedReward = true;
            }
            else {
                InventoryManager.instance.RemoveItem(_quest.getReward());
            }
        }

    }

    private void ShowMissionNotification() {
        InventoryManager.instance.AddQuest(_quest, _npcName);
        DialogueSystem.instance.OnDialogueEnd -= ShowMissionNotification;
    }

    private void GiveReward() {
        InventoryManager.instance.AddItem(_quest.getReward());
        //InventoryManager.instance.RemoveQuest(_quest);
        DialogueSystem.instance.OnDialogueEnd -= GiveReward;
    }
}
