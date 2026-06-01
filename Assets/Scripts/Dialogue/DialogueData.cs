using System.Collections.Generic;

[System.Serializable]
public class DialogueEntry
{
    public string id;
    public string speakerName;
    public List<string> lines;
}

[System.Serializable]
public class DialogueDatabase
{
    public List<DialogueEntry> dialogues;
}
