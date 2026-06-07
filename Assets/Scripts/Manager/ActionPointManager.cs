using System.Collections.Generic;
using UnityEngine;

public class ActionPointManager : MonoBehaviour
{
    public static ActionPointManager instance { get; private set; }

    

    public LifePhase currentPhase = LifePhase.Infancy;
    public int CurrentAP { get; private set; }
    public int MaxAP => GameConfig.MaxAPByPhase[currentPhase];
    private int _apModifier = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        ResetDay();
    }

    public void ResetDay()
    {
        CurrentAP = Mathf.Max(0, MaxAP + _apModifier);
        Debug.Log($"[AP] 새 날 시작 - 행동력: {CurrentAP} (기본 {MaxAP} {_apModifier:+0;-0})");
    }

    public bool UseAP(int cost)
    {
        if (CurrentAP < cost)
        {
            Debug.Log("[AP] 행동력 부족!");
            return false;
        }
        CurrentAP -= cost;
        Debug.Log($"[AP] -{cost} → 남은 행동력: {CurrentAP}");
        return true;
    }
    
    public bool HasAP(int cost) => CurrentAP >= cost;

    public void SetHappinessModifier(int modifier)
    {
        _apModifier = modifier;
    }
}
