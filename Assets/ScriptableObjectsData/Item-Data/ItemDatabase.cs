using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/ItemDatabase")]
public class ItemDatabase : ScriptableObject{
    public List<ItemInfo> allItens;
    private Dictionary<string, ItemInfo> _dataBase;

    public void Initialize() {
        _dataBase = allItens.ToDictionary(x => x.itemName, x => x);
    }

    public ItemInfo GetItemByName(string id) {
        if (_dataBase == null)
            Initialize();
        return _dataBase.TryGetValue(id, out var item) ? item : null;
    }
}
