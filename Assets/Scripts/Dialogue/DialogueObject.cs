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
    [SerializeField] private GameObject interactionMarkPrefab;
    private GameObject _indicator;

    private void Start()
    {
        _indicator = Instantiate(interactionMarkPrefab, transform);
        _indicator.transform.localPosition = new Vector3(0, 0.7f, 0);
    }

    public void SetInteracted(bool interacted)
    {
        _indicator.SetActive(!interacted);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

}
