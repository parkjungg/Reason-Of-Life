using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerInput _input;
    private DialogueObject _nearbyTarget;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (!_input.InteractPressed) return;

        if (DialogueManager.instance.IsDialoguing)
        {
            DialogueManager.instance.Advance();
            return;
        }

        if (_nearbyTarget != null)
        {
            int day = 0;
            string id = _nearbyTarget.dialogueIdByDay[day];
            DialogueManager.instance.StartDialogue(id);
        }
    }

    private void FixedUpdate()
    {
        _nearbyTarget = null;
        float minDist = float.MaxValue;

        foreach (var obj in FindObjectsByType<DialogueObject>(FindObjectsSortMode.None))
        {
            float dist = Vector2.Distance(transform.position, obj.transform.position);
            if (dist <= obj.interactionRadius && dist < minDist)
            {
                minDist = dist;
                _nearbyTarget = obj;
            }
        }
    }
}
