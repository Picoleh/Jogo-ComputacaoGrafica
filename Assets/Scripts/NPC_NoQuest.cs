using UnityEngine;

public class NPC_NoQuest : MonoBehaviour, IInteractable {

    [SerializeField] private string _npcName;
    [SerializeField] private Dialogue dialogue;

    public string interactionPrompt => "Conversar";


    public void Interact(Interactor interactor) {
        DialogueSystem.instance.StartDialogue(_npcName, dialogue);
    }
}
