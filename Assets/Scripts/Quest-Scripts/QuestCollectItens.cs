using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/QuestCollectItens")]
public class QuestCollectItens : QuestBase {

    public List<ItemInfo> _itensToCollect;
    public List<GameObject> prefabs;
    public List<Vector3> positions;
    private GameObject instancia;

    public override void StartQuest() {
        if (prefabs.Count != positions.Count) {
            Debug.Log("Numero de prefabs != numero de posicoes at QuestCollectItens");
            return;
        }
        DialogueSystem.instance.OnDialogueEnd += AddQuestToInventory;
        for(int i = 0; i < prefabs.Count; i++) {
            GameObject.Instantiate(prefabs[i], positions[i], Quaternion.identity);
        }

        _completed = false;
    }

    public override bool CheckComplete() {
        if (_completed) {
            return true;
        }
        else {
            if (InventoryManager.instance.HasItens(_itensToCollect)) {
                DialogueSystem.instance.OnDialogueEnd += GiveReward;
                InventoryManager.instance.RemoveQuest(this);
                InventoryManager.instance.RemoveItens(_itensToCollect);
                DialogueSystem.instance.OnDialogueEnd += ShowSucessNotification;
                _completed = true;
                return true;
            }
            return false;
        }
    }

    public override void RedoQuest() {
        foreach (var item in _itensToCollect) {
            if(InventoryManager.instance.HasItem(item))
                InventoryManager.instance.RemoveItem(item);
        }
        StartQuest();
        AddQuestToInventory();
    }
}
