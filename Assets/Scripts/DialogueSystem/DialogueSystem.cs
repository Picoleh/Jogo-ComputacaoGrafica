using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {
    public static DialogueSystem instance { get; private set; }

    private Queue<DialogueLine> sentences;
    private Coroutine typingCoroutine;

    [SerializeField] private GameObject _dialogueCanvas;
    [SerializeField] private TextMeshProUGUI _textName;
    [SerializeField] private TextMeshProUGUI _textSentence;
    [SerializeField] private float typingSpeed = 0.03f; // tempo entre letras
    [SerializeField] private const int maxCharByPage = 60;
    private bool isTyping = false;
    private string currentNPCName;

    public event Action OnDialogueEnd;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        currentNPCName = null;
        sentences = new Queue<DialogueLine>();
        _dialogueCanvas.SetActive(false);
    }

    public void StartDialogue(string npcName, Dialogue dialogue) {
        InputMapManager.instance.EnableMap("Dialogue");
        sentences.Clear();
        foreach (DialogueLine line in dialogue.lines) {
            sentences.Enqueue(line);
        }

        _dialogueCanvas.SetActive(true);
        currentNPCName=npcName;
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        
        if (isTyping) {
            StopCoroutine(typingCoroutine);
            _textSentence.maxVisibleCharacters = _textSentence.text.Length;
            isTyping = false;
            return;
        }

        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        
        DialogueLine sentence = sentences.Dequeue();
        _textName.text = sentence.speaker == DialogueLine.Speaker.NPC ? currentNPCName : "Beet";

        typingCoroutine = StartCoroutine(TypeSentence(sentence.text));

    }

    private IEnumerator TypeSentence(string sentence) {
        isTyping = true;
        _textSentence.text = sentence;
        _textSentence.ForceMeshUpdate();                
        _textSentence.maxVisibleCharacters = 0;         
        yield return null;

        _textSentence.ForceMeshUpdate();
        int totalVisible = _textSentence.textInfo.characterCount;
        int count = 0;

        while (count < totalVisible) {
            count++;
            _textSentence.maxVisibleCharacters = count;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void EndDialogue() {
        _dialogueCanvas.SetActive(false);
        InputMapManager.instance.EnableMap("Gameplay");
        OnDialogueEnd?.Invoke();
    }
}
