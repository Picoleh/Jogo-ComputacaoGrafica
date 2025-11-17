using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : SlotSelectableUI{
    [SerializeField] private TextMeshProUGUI _questOwnerText;
    [SerializeField] private TextMeshProUGUI _questTitleText;
    private QuestBase _quest;
    private string _ownerName;

    public bool IsEmpty => _quest == null;

    private void Awake() {
        gameObject.SetActive(false);
        highlightImage.enabled = false;
    }

    public void SetQuest(QuestBase quest) {
        _quest = quest;
        _ownerName = quest.getOwnerName();
        _questTitleText.text = quest.questTitle;
        _questOwnerText.text = quest.getOwnerName();

        SetUIInfo(quest.questTitle, quest.description);
        //_descriptionText = quest.getQuestDescription();
        //_titleText = quest.getQuestTitle();
    }

    public void ClearSlot() {
        _quest = null;
        _ownerName = null;
    }

    public QuestBase getQuest() {
        return _quest;
    }

    public string getOwnerName() {
        return _ownerName;
    }
}
