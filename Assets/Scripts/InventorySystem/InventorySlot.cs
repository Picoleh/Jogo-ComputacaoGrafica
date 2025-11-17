using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : SlotSelectableUI{
    [SerializeField] private Image _icon;
    [SerializeField] private ItemInfo _item;

    public bool IsEmpty => _item == null;

    private void Awake() {
        //_icon.enabled = false;
    }

    public void SetItem(ItemInfo item) {
        //_icon.enabled = true;
        _item = item;
        _icon.sprite = item.icon;

        SetUIInfo(_item.itemName, _item.itemDescription);
        //_descriptionText = _item.itemDescription;
        //_titleText = _item.itemName;
    }

    public void ClearSlot(){
        //_icon.enabled = false;
        _item = null;
        _icon.sprite = null;
    }

    public ItemInfo getItem() {
        return _item;
    }
}
