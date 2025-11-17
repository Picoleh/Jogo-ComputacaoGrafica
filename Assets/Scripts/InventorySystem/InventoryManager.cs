using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, ISaveable
{
    public static InventoryManager instance;

    [SerializeField] private GameObject _invUI;
    [SerializeField] private TextMeshProUGUI descTitleRef;
    [SerializeField] private TextMeshProUGUI descriptionRef;
    [SerializeField] private ItemDatabase _itemDatabase;
    [SerializeField] private QuestDatabase _questDatabase;
    private List<InventorySlot> itensSlots;
    private List<QuestSlot> questSlots;

    //private bool isOpen;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SaveManager.instance.RegisterInventory(this);
        //isOpen = false;
        _invUI.SetActive(false);

        descTitleRef.text = "";
        descriptionRef.text = "";


        itensSlots = gameObject.GetComponentsInChildren<InventorySlot>(true).ToList();
        questSlots = gameObject.GetComponentsInChildren<QuestSlot>(true).ToList();

        foreach (var slot in itensSlots) {
            slot.SetDependencies(descriptionRef, descTitleRef);
        }
        foreach (var slot in questSlots) {
            slot.SetDependencies(descriptionRef, descTitleRef);
        }
    }

    private void OnEnable() {
        foreach (QuestSlot slot in questSlots) { 
            if(slot.IsEmpty)
                slot.gameObject.SetActive(false);
            else
                slot.gameObject.SetActive(true);
        }

        foreach (InventorySlot slot in itensSlots) { 
            if(slot.IsEmpty)
                slot.gameObject.SetActive(false);
            else
                slot.gameObject.SetActive(true);
        }
    }

    public void AddItem(ItemInfo item) {
        foreach (var slot in itensSlots) {
            if (slot.IsEmpty) {
                slot.SetItem(item);
                NotificationManager.instance.ShowNotification("Novo Item:\nAperte TAB para ver");
                return;
            }
        }
    }

    public void RemoveItem(ItemInfo item) {
        for (int i = 0; i < itensSlots.Count; i++) {
            if (itensSlots[i].getItem() == item) {
                itensSlots[i].ClearSlot();
                for(int j = i + 1; j < itensSlots.Count; j++) {
                    if (itensSlots[j].getItem() == null)
                        break;
                    itensSlots[j - 1].SetItem(itensSlots[j].getItem());
                    itensSlots[j].ClearSlot();
                }
                break;
            }
        }
    }

    public void RemoveItens(List<ItemInfo> itens) {
        foreach (ItemInfo item in itens) {
            RemoveItem(item);
        }
    }

    public void AddQuest(QuestBase quest) {
        foreach(var slot in questSlots) {
            if (slot.IsEmpty) { 
                slot.SetQuest(quest);
                NotificationManager.instance.ShowNotification("Nova Quest:\nAperte TAB para ver");
                return;
            }
        }
    }

    public void RemoveQuest(QuestBase quest) {
        for (int i = 0; i < questSlots.Count; i++) {
            if (questSlots[i].getQuest() == quest) {
                questSlots[i].ClearSlot();
                for (int j = i + 1; j < questSlots.Count; j++) {
                    if (questSlots[j].getQuest() == null)
                        break;
                    questSlots[j - 1].SetQuest(questSlots[j].getQuest());
                    questSlots[j].ClearSlot();
                }
                break;
            }
        }
    }

    public void OpenInventory() {
        InputMapManager.instance.EnableMap("Inventory");
        Cursor.lockState = CursorLockMode.None;
        _invUI.SetActive(true);

        foreach (var slot in itensSlots) {
            if (slot.gameObject.activeSelf) {
                EventSystem.current.SetSelectedGameObject(slot.gameObject);
                break;
            }
        }
    }

    public void OnCloseInventory(InputAction.CallbackContext context) {
        InputMapManager.instance.EnableMap("Gameplay");
        _invUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public object GetData() {
        List<string> itensNames = new List<string>();
        List<int> questsIds = new List<int>();
        foreach (var slot in itensSlots) { 
            if(!slot.IsEmpty)
                itensNames.Add(slot.getItem().itemName);
        }
        //foreach(var slot in questSlots) {
        //    if (!slot.IsEmpty)
        //        questsIds.Add(slot.getQuest().id);
        //}
        return new InventoryData(itensNames);
    }

    public void SetData(object data) {
        InventoryData invData = (InventoryData)data;
        foreach (var item in invData.itens) {
            AddItem(_itemDatabase.GetItemByName(item));
        }

        //foreach (var questId in invData.quests) {
        //    AddQuest(_questDatabase.GetQuestById(questId));
        //}
    }

    public bool HasItens(List<ItemInfo> itensToTest) {
        foreach (var item in itensToTest) { 
            if(!HasItem(item)) 
                return false;
        }
        return true;
    }

    public bool HasItem(ItemInfo itemToTest) {
        foreach (var slot in itensSlots) {
            if(!slot.IsEmpty)
                if(slot.getItem() == itemToTest)
                    return true;
        }
        return false;
    }
}
