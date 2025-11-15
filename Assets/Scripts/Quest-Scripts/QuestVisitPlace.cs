using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/QuestVisitPlace")]
public class QuestVisitPlace : QuestBase {

    public string promptOnTarget;
    public string messageInteractedTarget;
    public List<TargetIndicator> prefabsTarget;
    public List<Vector3> positions;
    private List<GameObject> instancias;

    public override void StartQuest() {
        instancias = new List<GameObject>();
        if (prefabsTarget.Count != positions.Count) {
            Debug.Log("Numero de prefabsTarget != numero de posicoes at QuestVisitPlace");
            return;
        }
        DialogueSystem.instance.OnDialogueEnd += AddQuestToInventory;
        for (int i = 0; i < prefabsTarget.Count; i++) {
            TargetIndicator instancia = GameObject.Instantiate(prefabsTarget[i], positions[i], Quaternion.identity);

            instancias.Add(instancia.gameObject);

            instancia.SetPrompt(promptOnTarget);
            instancia.SetTargetInteractedNotificationMessage(messageInteractedTarget);
        }
    }

    public override bool CheckComplete() {
        if (_completed) {
            return true;
        }
        else { 
            if (instancias.Count > 0)
                return false;

            DialogueSystem.instance.OnDialogueEnd += GiveReward;
            InventoryManager.instance.RemoveQuest(this);
            DialogueSystem.instance.OnDialogueEnd += ShowSucessNotification;
            _completed = true;
            return true;
        }
    }

    public override void RedoQuest() {
        StartQuest();
        AddQuestToInventory();
    }
}
