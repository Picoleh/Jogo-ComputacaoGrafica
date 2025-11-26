using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum NotificationIcon {
    Add,
    Complete,
    Interacted,
    Activated
}

public class Notification {
    public string message;
    public NotificationIcon icon;

    public Notification(string message, NotificationIcon icon) {
        this.message = message;
        this.icon = icon;
    }
}

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance { get; private set; }
    [SerializeField] private CanvasGroup _notificationCanvas;
    [SerializeField] private TextMeshProUGUI _notificationText;
    [SerializeField] private Image _icon;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayTime = 2f;
    [Header("Mesma ordem enum")]
    [SerializeField] private List<Sprite> iconsImages = new List<Sprite>();

    private Queue<Notification> notificationQueue = new();
    private bool isShowing = false;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;

        gameObject.SetActive(false);
    }

    public void ShowNotification(string message, NotificationIcon icon) {
        gameObject.SetActive(true);
        notificationQueue.Enqueue(new Notification(message, icon));
        // Se nenhuma notificação está sendo exibida, processa
        if (!isShowing)
            StartCoroutine(ProcessQueue());
    }

    private Sprite GetIconByEnum(NotificationIcon icon) {
        return iconsImages[(int)icon];
    }

    private IEnumerator ProcessQueue() {
        isShowing = true;
        Debug.Log("Processando");
        while (notificationQueue.Count > 0) {
            Notification n = notificationQueue.Dequeue();
            yield return StartCoroutine(FadeRoutine(n));
        }

        isShowing = false;
        gameObject.SetActive(false);
        Debug.Log("Processado");
    }

    private IEnumerator FadeRoutine(Notification n) {
        _notificationText.text = n.message;
        _icon.sprite = GetIconByEnum(n.icon);
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
