using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDatabase", menuName = "Scriptable Objects/QuestDatabase")]
public class QuestDatabase : ScriptableObject{
    public List<QuestBase> allQuests;
    private Dictionary<int, QuestBase> _dataBase;

    public void Initialize() {
        _dataBase = allQuests.ToDictionary(x => x.id, x => x);
    }

    public QuestBase GetQuestById(int id) {
        if (_dataBase == null)
            Initialize();
        return _dataBase.TryGetValue(id, out var item) ? item : null;
    }
}
