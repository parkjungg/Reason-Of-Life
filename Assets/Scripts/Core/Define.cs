using System.Collections.Generic;

public enum LifePhase
{
    Infancy,
    Adolescence,
    Adult,
    Elderly
}

public static class GameConfig
{
    public static readonly Dictionary<LifePhase, int> PhaseDuration = new()
    {
        { LifePhase.Infancy, 2 },
        { LifePhase.Adolescence, 3 },
        { LifePhase.Adult, 3 },
        { LifePhase.Elderly, 2 }
    };
}
