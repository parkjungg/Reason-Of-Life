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
    public float interactionRadius = 1.5f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

}
