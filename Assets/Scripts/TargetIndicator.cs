using UnityEngine;

public class TargetIndicator : MonoBehaviour, IInteractable, ISaveable {
    private string prompt;
    private string targetInteractedNotification;

    public string interactionPrompt => string.IsNullOrEmpty(prompt) ? "Interagir" : prompt;

    public object GetData() {
        throw new System.NotImplementedException();
    }

    public void SetData(object data) {
        throw new System.NotImplementedException();
    }

    public void Interact(Interactor interactor) {
        NotificationManager.instance.ShowNotification(string.IsNullOrEmpty(targetInteractedNotification) ? "Interagido" : targetInteractedNotification);
        Destroy(gameObject);
    }

    public void SetPrompt(string prompt) {
        this.prompt = prompt;
    }

    public void SetTargetInteractedNotificationMessage(string message) {
        targetInteractedNotification = message;
    }
}
