using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject : MonoBehaviour
{
    [Header("Json File Name")]
    public string jsonFileName = "dialogues_in_house";
    
    [Header("Dialogue")] 
    public string[] dialogueIdByDay;

    [Header("Interaction")]
    public bool isBed = false;
    public float interactionRadius = 1.5f;
    public int[] availableDays;
    
    [SerializeField] private GameObject interactionMarkPrefab;
    private GameObject _indicator;

    private void Start()
    {
        if (!IsAvailableToday())
        {
            gameObject.SetActive(false);
            return;
        }
        _indicator = Instantiate(interactionMarkPrefab, transform);
        _indicator.transform.localPosition = new Vector3(0, 0.7f, 0);
        
        int day = GameManager.instance.currentDay - 1;
        bool hasDialogue = !isBed && day < dialogueIdByDay.Length && !string.IsNullOrEmpty(dialogueIdByDay[day]);
        _indicator.SetActive(hasDialogue);
    }

    public bool IsAvailableToday()
    {
        if (availableDays == null || availableDays.Length == 0) return true;
        
        int today = GameManager.instance.currentDay;
        foreach (int day in availableDays)
        {
            if (day == today) return true;
        }
        return false;
    }

    public void SetInteracted(bool interacted)
    {
        _indicator.SetActive(!interacted);
    }

    // 추후 제거 (디버그용)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

}
