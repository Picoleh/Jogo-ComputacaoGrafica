using TMPro;
using UnityEngine;

public class InteractionUIPrompt: MonoBehaviour{
    private Camera _mainCamera;
    [SerializeField] private GameObject _interactionPromptPanel;
    [SerializeField] private TextMeshProUGUI _promptText;
    public bool isActive;

    private void Start() {
        _mainCamera = Camera.main;
        _interactionPromptPanel.SetActive(false);
        isActive = false;
    }

    private void LateUpdate() {
        var rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public void SetUp(string promptText) {
        _promptText.text = promptText;
        _interactionPromptPanel.SetActive(true);
        isActive = true;
    }

    public void Close() {
        _interactionPromptPanel.SetActive(false);
        isActive = false;
    }
}
