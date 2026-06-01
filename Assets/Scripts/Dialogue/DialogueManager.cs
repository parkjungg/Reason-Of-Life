using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }

    [Header("UI References")] 
    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private List<string> _currentLines;
    private int _lineIndex;
    public bool IsDialoguing { get; private set; }
    private DialogueDatabase _database;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        LoadDatabase();
        dialoguePanel.SetActive(false);
    }

    private void LoadDatabase()
    {
        string sceneName = SceneManager.GetActiveScene().name.ToLower();
        TextAsset json = Resources.Load<TextAsset>($"Dialogue/dialogues_{sceneName}");
        if (json == null) { Debug.LogError($"dialogues_{sceneName}.json 없음"); return; }
        _database = JsonUtility.FromJson<DialogueDatabase>(json.text);
    }

    public void StartDialogue(string id)
    {
        if (IsDialoguing) return;
        DialogueEntry entry = _database?.dialogues.Find(d => d.id == id);
        if(entry == null) { Debug.LogWarning($"id '{id}' 없음"); return; }
        
        _currentLines = entry.lines;
        _lineIndex = 0;
        speakerText.text = entry.speakerName;
        IsDialoguing = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = _currentLines[0];
    }

    public void Advance()
    {
        if(!IsDialoguing) return;
        _lineIndex++;
        if(_lineIndex < _currentLines.Count)
            dialogueText.text = _currentLines[_lineIndex];
        else
        {
            IsDialoguing = false;
            dialoguePanel.SetActive(false);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadDatabase();
    }
}
