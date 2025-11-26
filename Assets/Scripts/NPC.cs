using System;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable, ISaveable{
    [SerializeField] private string _npcName;
    [SerializeField] private string _prompt;
    [SerializeField] private QuestBase _quest;
    [SerializeField] private Animator _animator;
    [SerializeField] private Walker _walker;
    [SerializeField] private AudioClip _voice;
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
            DialogueSystem.instance.StartDialogue(_npcName, _quest.questDialogue, _voice);

            if(_walker != null)
                DialogueSystem.instance.OnDialogueEnd += BeginWalker;
            _firstInteraction = false;
            if(_quest != null)
                _quest.StartQuest();
        }
        else {
            DialogueSystem.instance.StartDialogue(_npcName, _quest.getCombackDialog(), _voice);
        }

    }

    private void BeginWalker() {
        _walker.startWalker();
        DialogueSystem.instance.OnDialogueEnd -= BeginWalker;
    }

    public object GetData() {
        return new NPCData(_firstInteraction, _quest._completed);
    }

    public void SetData(object data) {
        NPCData nPCData = (NPCData)data;

        _firstInteraction = nPCData.firstInteraction;
        _quest._completed = nPCData.questCompleted;

        if (!_firstInteraction) {
            if(_npcName == "Tonclay") {
                transform.position = new Vector3(48f, 0f, -33.75f);
                transform.Rotate(0f, 56.25f, 0f);
            }

            if(!_quest._completed)
                _quest.RedoQuest();
        }
    }
}
