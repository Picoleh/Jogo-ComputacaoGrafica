using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable, ISaveable{
    [SerializeField] private string _npcName;
    [SerializeField] private string _prompt;
    [SerializeField] private QuestBase _quest;
    [SerializeField] private Animator _animator; 
    private bool _firstInteraction = true;

    public string interactionPrompt => _prompt;

    private void Awake() {
        SaveManager.instance.RegisterNPC(this);
        if(_quest != null)
            _quest.setOwnerName(_npcName);
    }

    public void Interact(Interactor interactor) {
        _animator.SetTrigger("Interacted");
        //_animator.ResetTrigger("Interacted");
        if (_firstInteraction) { 
            DialogueSystem.instance.StartDialogue(_npcName, _quest.questDialogue);
            _firstInteraction = false;
            //DialogueSystem.instance.OnDialogueEnd += ShowMissionNotification;
            if(_quest != null)
                _quest.StartQuest();
        }
        else {
            DialogueSystem.instance.StartDialogue(_npcName, _quest.getCombackDialog());
            //if (_quest.isCompleted() && !_receivedReward) {
            //    DialogueSystem.instance.OnDialogueEnd += GiveReward;
            //    _receivedReward = true;
            //}
            //else {
            //    InventoryManager.instance.RemoveItem(_quest.getReward());
            //}
        }

    }

    private void ShowMissionNotification() {
        InventoryManager.instance.AddQuest(_quest);
        DialogueSystem.instance.OnDialogueEnd -= ShowMissionNotification;
    }

    private void GiveReward() {
        InventoryManager.instance.AddItem(_quest.getReward());
        //InventoryManager.instance.RemoveQuest(_quest);
        DialogueSystem.instance.OnDialogueEnd -= GiveReward;
    }

    public object GetData() {
        return new NPCData(_firstInteraction, _quest._completed);
    }

    public void SetData(object data) {
        NPCData nPCData = (NPCData)data;

        _firstInteraction = nPCData.firstInteraction;

        if (!_firstInteraction && !_quest._completed) { 
            _quest.RedoQuest();
        }
    }
}
