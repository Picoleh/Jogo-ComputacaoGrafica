using UnityEngine;

public class Item : MonoBehaviour, IInteractable{

    [SerializeField] private ItemInfo _item;

    public string interactionPrompt => "Pegar";

    public void Interact(Interactor interactor) {
        Destroy(gameObject);
        InventoryManager.instance.AddItem(_item);
    }
}
