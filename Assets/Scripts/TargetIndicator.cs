using UnityEngine;

public class TargetIndicator : MonoBehaviour, IInteractable {
    private string prompt;
    private string targetInteractedNotification;
    private GameObject spawnAtTarget;
    [SerializeField] private bool destroyOnInteract;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip audioClip;
    public bool interacted = false;

    public string interactionPrompt => string.IsNullOrEmpty(prompt) ? "Interagir" : prompt;

    public void Interact(Interactor interactor) {
        NotificationManager.instance.ShowNotification(
            string.IsNullOrEmpty(targetInteractedNotification) ? "Interagido" : targetInteractedNotification,
            NotificationIcon.Add
        );
        if(spawnAtTarget != null)
            GameObject.Instantiate(spawnAtTarget, transform.position, Quaternion.identity);

        if(destroyOnInteract)
            Destroy(gameObject);

        if(animator != null) {
            animator.SetTrigger("Interacted");
        }

        if(audioClip != null)
            SoundManager.instance.PlaySFX(audioClip);

        interacted = true;
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
