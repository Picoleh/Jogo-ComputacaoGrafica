using System.Collections;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance { get; private set; }
    [SerializeField] private CanvasGroup _notificationCanvas;
    [SerializeField] private TextMeshProUGUI _notificationText;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayTime = 2f;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;

        gameObject.SetActive(false);
    }

    public void ShowNotification(string message) {
        _notificationText.text = message;
        gameObject.SetActive(true);
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine() {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime) {
            _notificationCanvas.alpha = t / fadeDuration;
            yield return null;
        }
        _notificationCanvas.alpha = 1f;

        yield return new WaitForSeconds(displayTime);

        for (float t = 0; t < fadeDuration; t += Time.deltaTime) {
            _notificationCanvas.alpha = 1 - (t / fadeDuration);
            yield return null;
        }
        _notificationCanvas.alpha = 0f;

        gameObject.SetActive(false);
    }
}
