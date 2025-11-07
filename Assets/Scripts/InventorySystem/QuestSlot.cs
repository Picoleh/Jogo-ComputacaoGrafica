using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : SlotSelectableUI{
    [SerializeField] private TextMeshProUGUI _questOwnerText;
    [SerializeField] private TextMeshProUGUI _questTitleText;
    private Quest _quest;
    private string _ownerName;

    public bool IsEmpty => _quest == null;

    private void Awake() {
        gameObject.SetActive(false);
        highlightImage.enabled = false;
    }

    public void SetQuest(Quest quest, string ownerName) {
        _quest = quest;
        _ownerName = ownerName;
        _questTitleText.text = quest.getQuestTitle();
        _questOwnerText.text = ownerName;

        SetUIInfo(quest.getQuestTitle(), quest.getQuestDescription());
        //_descriptionText = quest.getQuestDescription();
        //_titleText = quest.getQuestTitle();
    }

    public void ClearSlot() {
        _quest = null;
        _ownerName = null;
    }

    public Quest getQuest() {
        return _quest;
    }

    public string getOwnerName() {
        return _ownerName;
    }
}
