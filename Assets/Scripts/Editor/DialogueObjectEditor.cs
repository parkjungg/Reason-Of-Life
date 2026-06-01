using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueObject))]
public class DialogueObjectEditor : Editor
{
    private static string GetJsonPath(string fileName) =>
        Path.Combine(Application.dataPath, $"Resources/Dialogue/{fileName}.json");

    private DialogueDatabase _database;
    private string _loadedFileName;

    private void OnEnable()
    {
        DialogueObject obj = (DialogueObject)target;
        LoadDatabase(obj.jsonFileName);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueObject obj = (DialogueObject)target;

        // 파일명이 바뀌면 재로드
        if (_loadedFileName != obj.jsonFileName)
            LoadDatabase(obj.jsonFileName);

        EditorGUILayout.Space(8);
        EditorGUILayout.LabelField("── 대사 편집 ──", EditorStyles.boldLabel);

        if (obj.dialogueIdByDay == null || obj.dialogueIdByDay.Length == 0)
        {
            EditorGUILayout.HelpBox("dialogueIdByDay 배열에 id를 먼저 입력하세요.", MessageType.Info);
            return;
        }

        for (int day = 0; day < obj.dialogueIdByDay.Length; day++)
        {
            string id = obj.dialogueIdByDay[day];
            if (string.IsNullOrEmpty(id)) continue;

            EditorGUILayout.Space(4);
            EditorGUILayout.LabelField($"[ Day {day + 1} ] id: {id}", EditorStyles.boldLabel);

            DialogueEntry entry = _database?.dialogues.Find(d => d.id == id);

            if (entry == null)
            {
                EditorGUILayout.HelpBox($"id '{id}' 가 JSON에 없습니다.", MessageType.Warning);
                if (GUILayout.Button($"'{id}' JSON에 추가"))
                {
                    _database.dialogues.Add(new DialogueEntry
                    {
                        id = id,
                        speakerName = "이름",
                        lines = new List<string> { "첫 번째 대사." }
                    });
                    SaveDatabase(obj.jsonFileName);
                }
                continue;
            }

            entry.speakerName = EditorGUILayout.TextField("화자 이름", entry.speakerName);

            for (int i = 0; i < entry.lines.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                entry.lines[i] = EditorGUILayout.TextField($"  [{i}]", entry.lines[i]);
                if (GUILayout.Button("✕", GUILayout.Width(24))) { entry.lines.RemoveAt(i); break; }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("+ 대사 추가")) entry.lines.Add("새 대사");
        }

        EditorGUILayout.Space(8);
        if (GUILayout.Button("💾  JSON 저장", GUILayout.Height(30)))
            SaveDatabase(obj.jsonFileName);
    }

    private void LoadDatabase(string fileName)
    {
        _loadedFileName = fileName;
        string path = GetJsonPath(fileName);
        if (!File.Exists(path))
        {
            _database = new DialogueDatabase { dialogues = new List<DialogueEntry>() };
            return;
        }
        _database = JsonUtility.FromJson<DialogueDatabase>(File.ReadAllText(path));
        if (_database.dialogues == null) _database.dialogues = new List<DialogueEntry>();
    }

    private void SaveDatabase(string fileName)
    {
        File.WriteAllText(GetJsonPath(fileName), JsonUtility.ToJson(_database, true));
        AssetDatabase.Refresh();
        Debug.Log($"{fileName}.json 저장 완료");
    }
}