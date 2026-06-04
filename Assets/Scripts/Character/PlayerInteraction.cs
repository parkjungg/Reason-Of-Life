using System.Collections.Generic;
using UnityEngine;
public class PlayerInteraction : MonoBehaviour
{
    private DialogueObject _nearbyTarget;
    private bool _isInteracting;
    private HashSet<DialogueObject> _interactedObjects = new();

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        if (DialogueManager.instance.IsDialoguing)
        {
            DialogueManager.instance.Advance();

            if (!DialogueManager.instance.IsDialoguing && _isInteracting)
            {
                if (!_interactedObjects.Contains(_nearbyTarget))
                {
                    ActionPointManager.instance.UseAP(3);
                    _interactedObjects.Add(_nearbyTarget);
                    _nearbyTarget.SetInteracted(true);
                }
                _isInteracting = false;
            }
            return;
        }

        if (_nearbyTarget == null) return;

        if (_nearbyTarget.isBed)
        {
            GameManager.instance.Sleep();
            return;
        }
        
        bool alreadyInteracted = _interactedObjects.Contains(_nearbyTarget);
            
        if (!alreadyInteracted && !ActionPointManager.instance.HasAP(3))
        {
            Debug.Log("행동력이 부족해서 상호작용 불가");
            // TODO : 행동력이 부족하다는 UI 띄우기
            return;
        }
        int day = 0;
        string id = _nearbyTarget.dialogueIdByDay[day];
        DialogueManager.instance.StartDialogue(id);
        _isInteracting = true;
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
