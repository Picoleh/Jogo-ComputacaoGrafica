using UnityEngine;

public interface IInteractable{
    public string interactionPrompt { get;}

    public void Interact(Interactor interactor);
}
