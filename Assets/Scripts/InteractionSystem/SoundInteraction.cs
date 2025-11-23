using UnityEngine;

public class SoundInteraction : MonoBehaviour, IInteractable {
    [SerializeField] private string prompt;
    [SerializeField] private AudioClip sound;
    public string interactionPrompt => prompt;

    public void Interact(Interactor interactor) {
        SoundManager.instance.PlaySFX(sound);
    }
}
