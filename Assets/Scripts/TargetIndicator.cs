using UnityEngine;

public class TargetIndicator : MonoBehaviour, IInteractable {
    private string prompt;
    private string targetInteractedNotification;
    private GameObject spawnAtTarget;

    public string interactionPrompt => string.IsNullOrEmpty(prompt) ? "Interagir" : prompt;

    public void Interact(Interactor interactor) {
        NotificationManager.instance.ShowNotification(string.IsNullOrEmpty(targetInteractedNotification) ? "Interagido" : targetInteractedNotification);
        if(spawnAtTarget != null)
            GameObject.Instantiate(spawnAtTarget, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetPrompt(string prompt) {
        this.prompt = prompt;
    }

    public void SetTargetInteractedNotificationMessage(string message) {
        targetInteractedNotification = message;
    }

    public void SetSpawnAtTargetPrefab(GameObject gameObject) {
        spawnAtTarget = gameObject;
    }
}
