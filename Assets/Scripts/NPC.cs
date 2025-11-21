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
        if (_firstInteraction) { 
            DialogueSystem.instance.StartDialogue(_npcName, _quest.questDialogue);
            _firstInteraction = false;
            if(_quest != null)
                _quest.StartQuest();
        }
        else {
            DialogueSystem.instance.StartDialogue(_npcName, _quest.getCombackDialog());
        }

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
