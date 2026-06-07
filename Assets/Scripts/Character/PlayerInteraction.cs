using System.Collections.Generic;
using UnityEngine;
public class PlayerInteraction : MonoBehaviour
{
    private PlayerInput _input;
    private DialogueObject _nearbyTarget;
    private bool _isInteracting;
    private HashSet<DialogueObject> _interactedObjects = new();

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (!_input.InteractPressed) return;
        if (GameManager.instance.IsSleeping) return;

        if (HappinessManager.instance.CurrentState == HappinessState.Collapse)
        {
            if (_nearbyTarget != null && _nearbyTarget.isBed)
                GameManager.instance.Sleep();
            else if(_nearbyTarget != null && !_nearbyTarget.isBed)
                Debug.Log("잠에 드는게 좋겠다...");
                // TODO : 로그 대신 UI로 띄우기
            return;
        }

        if (DialogueManager.instance.IsDialoguing)
        {
            DialogueManager.instance.Advance();

            if (!DialogueManager.instance.IsDialoguing && _isInteracting)
            {
                if (!_interactedObjects.Contains(_nearbyTarget))
                {
                    HappinessManager.instance.ModifyHappiness(_nearbyTarget.happinessAP);
                    ActionPointManager.instance.UseAP(_nearbyTarget.apCost);
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
            
        if (!alreadyInteracted && !ActionPointManager.instance.HasAP(_nearbyTarget.apCost))
        {
            Debug.Log("행동력이 부족해서 상호작용 불가");
            // TODO : 행동력이 부족하다는 UI 띄우기
            return;
        }
        int day = GameManager.instance.currentDay - 1;
        
        if (day >= _nearbyTarget.dialogueIdByDay.Length)
        {
            Debug.LogWarning("해당 날짜의 대사 없음");
            return;
        }
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
