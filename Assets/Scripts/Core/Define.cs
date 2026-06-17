using System.Collections.Generic;

public enum HappinessState
{
    Happy,
    Stable,
    Bad,
    Depressed,
    Collapse
}
public enum LifePhase
{
    Infancy,
    Adolescence,
    Adult,
    Elderly
}

public static class GameConfig
{
    // 페이즈 진행일 수
    public static readonly Dictionary<LifePhase, int> PhaseDuration = new()
    {
        { LifePhase.Infancy, 3 },
        { LifePhase.Adolescence, 3 },
        { LifePhase.Adult, 3 },
        { LifePhase.Elderly, 2 }
    };
    
    // 각 페이즈 기본 행동력
    public static readonly Dictionary<LifePhase, int> MaxAPByPhase = new()
    {
        { LifePhase.Infancy, 5 },
        { LifePhase.Adolescence, 10 },
        { LifePhase.Adult, 10 },
        { LifePhase.Elderly, 7 }
    };
}
