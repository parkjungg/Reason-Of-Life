using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("행복도")]
    [SerializeField] private Slider happinessSlider;
    [SerializeField] private TextMeshProUGUI happinessText;

    [Header("행동력")]
    [SerializeField] private Slider apSlider;
    [SerializeField] private TextMeshProUGUI apText;

    [Header("페이즈")]
    [SerializeField] private TextMeshProUGUI phaseText;

    void Start()
    {
        HappinessManager.instance.OnValueChanged += UpdateHappiness;
        ActionPointManager.instance.OnAPChanged += UpdateAP;
        GameManager.instance.OnPhaseChanged += UpdatePhase;

        UpdateHappiness(HappinessManager.instance.happiness);
        UpdateAP(ActionPointManager.instance.CurrentAP);
        UpdatePhase(GameManager.instance.currentPhase);
    }

    void OnDestroy()
    {
        HappinessManager.instance.OnValueChanged -= UpdateHappiness;
        ActionPointManager.instance.OnAPChanged -= UpdateAP;
        GameManager.instance.OnPhaseChanged -= UpdatePhase;
    }

    void UpdateHappiness(float value)
    {
        happinessSlider.value = value;
        happinessText.text = $"{value:0}/{100}";
    }

    void UpdateAP(int value)
    {
        apSlider.maxValue = ActionPointManager.instance.ActualMaxAP;
        apSlider.value = value;
        apText.text = $"{value}/{ActionPointManager.instance.ActualMaxAP}";
    }

    void UpdatePhase(LifePhase phase)
    {
        phaseText.text = phase switch
        {
            LifePhase.Infancy     => "유아기",
            LifePhase.Adolescence => "청소년기",
            LifePhase.Adult       => "성인기",
            LifePhase.Elderly     => "노년기",
            _ => ""
        };
    }
}