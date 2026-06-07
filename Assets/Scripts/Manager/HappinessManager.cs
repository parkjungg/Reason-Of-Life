using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessManager : MonoBehaviour
{
    public static HappinessManager instance { get; private set; }

    [Range(0, 100)]
    public float happiness = 100f;
    
    public HappinessState CurrentState { get; private set; }

    public event Action<float> OnValueChanged;
    public event Action<HappinessState> OnStateChanged;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ModifyHappiness(float amount)
    {
        happiness = Mathf.Clamp(happiness + amount, 0f, 100f);
        OnValueChanged?.Invoke(happiness);
        UpdateState();
        Debug.Log($"행복도 변화: {amount:+0;-0} → 현재: {happiness} / 상태: {CurrentState}");
    }

    void UpdateState()
    {
        HappinessState newState = happiness switch
        {
            >= 80 => HappinessState.Happy,
            >= 50 => HappinessState.Stable,
            >= 30 => HappinessState.Bad,
            >= 5 => HappinessState.Depressed,
            _ => HappinessState.Collapse
        };

        if (newState != CurrentState)
        {
            CurrentState = newState;
            OnStateChanged?.Invoke(CurrentState);

            if (CurrentState == HappinessState.Collapse)
            {
                Debug.Log("붕괴상태입니다. 수면에 들어가야합니다!");
                DisableAllDialogueObjects();
            }
        }
    }

    void DisableAllDialogueObjects()
    {
        foreach (var obj in FindObjectsByType<DialogueObject>(FindObjectsSortMode.None))
        {
            if(!obj.isBed)
                obj.SetIndicatorVisible(false);
        }
    }
}
