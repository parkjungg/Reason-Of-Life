using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("InGame")]
    public int currentDay = 1;
    public LifePhase currentPhase = LifePhase.Infancy;
    private int _dayInPhase = 1;
    public bool IsSleeping { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Sleep()
    {
        if (IsSleeping) return;
        IsSleeping = true;
        StartCoroutine(SleepRoutine());
    }

    private IEnumerator SleepRoutine()
    {
        yield return StartCoroutine(FadeController.instance.FadeOut());

        AdvanceDay();
        HappinessManager.instance.ModifyHappiness(5f);

        int apModifier = HappinessManager.instance.CurrentState switch
        {
            HappinessState.Happy => 1,
            HappinessState.Stable => 0,
            HappinessState.Bad => -1,
            HappinessState.Depressed => -2,
            HappinessState.Collapse => -3,
            _ => 0
        };
        ActionPointManager.instance.SetHappinessModifier(apModifier);
        ActionPointManager.instance.currentPhase = currentPhase;
        ActionPointManager.instance.ResetDay();

        AsyncOperation op = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        yield return new WaitUntil(() => op.isDone);
        
        yield return StartCoroutine(FadeController.instance.FadeIn());

        IsSleeping = false;
    }

    private void AdvanceDay()
    {
        currentDay++;
        _dayInPhase++;

        if (_dayInPhase > GameConfig.PhaseDuration[currentPhase])
        {
            _dayInPhase = 1;
            AdvancePhase();
        }
        Debug.Log($"[Day] {currentDay}일차 / 페이즈: {currentPhase}");
    }

    private void AdvancePhase()
    {
        currentPhase = currentPhase switch
        {
            LifePhase.Infancy => LifePhase.Adolescence,
            LifePhase.Adolescence => LifePhase.Adult,
            LifePhase.Adult => LifePhase.Elderly,
            _ => currentPhase // 노년기 끝 = 게임 엔딩 처리 필요
        };
        Debug.Log($"[Phase] 페이즈 전환 → {currentPhase}");
    }
}
