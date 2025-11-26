using UnityEngine;

public class EasterEggItem : MonoBehaviour, IInteractable {

    public string interactionPrompt => "Ativar";
    private bool interacted = false;
    private static int active = 0;

    public void Interact(Interactor interactor) {
        if (!interacted) {
            active++;
            NotificationManager.instance.ShowNotification("Ativado (" + active.ToString() + "/3)", NotificationIcon.Activated);
            interacted = true;
            CheckEasterEgg();
        }
    }

    private void CheckEasterEgg() {
        if (active == 3) {
            Debug.Log("Easter EGG");
            SoundManager.instance.PlayEasterEggMusic();
        }
    }
}
