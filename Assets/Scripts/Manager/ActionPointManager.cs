using System.Collections.Generic;
using UnityEngine;

public class ActionPointManager : MonoBehaviour
{
    public static ActionPointManager instance { get; private set; }

    private static readonly Dictionary<LifePhase, int> MaxAPByPhase = new()
    {
        { LifePhase.Infancy, 5 },
        { LifePhase.Adolescence, 10 },
        { LifePhase.Adult, 10 },
        { LifePhase.Elderly, 7 }
    };

    public LifePhase currentPhase = LifePhase.Infancy;
    public int CurrentAP { get; private set; }
    public int MaxAP => MaxAPByPhase[currentPhase];

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
        CurrentAP = MaxAP;
        Debug.Log($"[AP] 새 날 시작 - 행동력 초기화: {CurrentAP}");
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
}
