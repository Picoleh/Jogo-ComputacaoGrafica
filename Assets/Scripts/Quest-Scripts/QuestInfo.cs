using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "Scriptable Objects/QuesInfo")]
public class QuestInfo : ScriptableObject{
    [TextArea(5, 10)]
    public string description;

    public Dialogue questDialogue;

    [Header("Options")]
    public Dialogue comebackInProgress;
    public Dialogue comebackFinished;

    [Header("Rewards")]
    public ItemInfo itemReward;
}
