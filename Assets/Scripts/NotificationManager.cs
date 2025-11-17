using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance { get; private set; }
    [SerializeField] private CanvasGroup _notificationCanvas;
    [SerializeField] private TextMeshProUGUI _notificationText;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayTime = 2f;

    private Queue<string> notificationQueue = new();
    private bool isShowing = false;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;

        gameObject.SetActive(false);
    }

    public void ShowNotification(string message) {
        gameObject.SetActive(true);
        notificationQueue.Enqueue(message);

        // Se nenhuma notificação está sendo exibida, processa
        if (!isShowing)
            StartCoroutine(ProcessQueue());
    }

    private IEnumerator ProcessQueue() {
        isShowing = true;
        Debug.Log("Processando");
        while (notificationQueue.Count > 0) {
            string msg = notificationQueue.Dequeue();
            yield return StartCoroutine(FadeRoutine(msg));
        }

        isShowing = false;
        gameObject.SetActive(false);
        Debug.Log("Processado");
    }

    private IEnumerator FadeRoutine(string message) {
        _notificationText.text = message;
        gameObject.SetActive(true);

        // FADE IN
        for (float t = 0; t < fadeDuration; t += Time.deltaTime) {
            _notificationCanvas.alpha = t / fadeDuration;
            yield return null;
        }
        _notificationCanvas.alpha = 1f;

        // TEMPO EXIBINDO
        yield return new WaitForSeconds(displayTime);

        // FADE OUT
        for (float t = 0; t < fadeDuration; t += Time.deltaTime) {
            _notificationCanvas.alpha = 1 - (t / fadeDuration);
            yield return null;
        }
        _notificationCanvas.alpha = 0f;
    }
}
