using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDatabase", menuName = "Scriptable Objects/QuestDatabase")]
public class QuestDatabase : ScriptableObject{
    public List<QuestInfo> allQuests;
    private Dictionary<int, QuestInfo> _dataBase;

    public void Initialize() {
        _dataBase = allQuests.ToDictionary(x => x.id, x => x);
    }

    public QuestInfo GetQuestById(int id) {
        if (_dataBase == null)
            Initialize();
        return _dataBase.TryGetValue(id, out var item) ? item : null;
    }
}
