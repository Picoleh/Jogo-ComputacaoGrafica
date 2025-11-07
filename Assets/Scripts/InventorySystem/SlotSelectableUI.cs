using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class SlotSelectableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler {
    protected string _descriptionText;
    protected string _titleText;
    private TextMeshProUGUI _descriptionDisplay;
    private TextMeshProUGUI _infoTitle;
    public Image highlightImage;

    void ShowDescription() {
        if (_descriptionDisplay != null) {
            _descriptionDisplay.text = _descriptionText;
            _infoTitle.text = _titleText;
        }

        if (highlightImage != null)
            highlightImage.enabled = true;
    }

    void HideDescription() {
        if (_descriptionDisplay != null) {
            _descriptionDisplay.text = "";
            _infoTitle.text = "";
        }

        if (highlightImage != null)
            highlightImage.enabled=false;
    }

    public void SetDependencies(TextMeshProUGUI descriptionText, TextMeshProUGUI titleText) {
        _descriptionDisplay = descriptionText;
        _infoTitle = titleText;
    }

    public void SetUIInfo(string title, string description) {
        _titleText = title;
        _descriptionText = description;
    }

    public void OnPointerEnter(PointerEventData e) => ShowDescription();
    public void OnPointerExit(PointerEventData e) => HideDescription();
    public void OnSelect(BaseEventData e) => ShowDescription();
    public void OnDeselect(BaseEventData e) => HideDescription();
}