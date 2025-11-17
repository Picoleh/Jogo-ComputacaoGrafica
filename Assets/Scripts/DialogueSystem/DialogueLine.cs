using UnityEngine;

[System.Serializable]
public class DialogueLine {
    public enum Speaker { NPC, Player }
    public Speaker speaker;
    [TextArea(2, 5)]
    public string text;
}