using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Scriptable Objects/ItemInfo")]
public class ItemInfo : ScriptableObject{
    public string itemName;

    [TextArea(3,5)]
    public string itemDescription;
    public Sprite icon;
}
