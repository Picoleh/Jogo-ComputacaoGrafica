using UnityEngine;

[System.Serializable]
public class DialogueLine {
    public enum Speaker { NPC, Player }
    public Speaker speaker;
    [TextArea(4, 8)]
    public string text;
}