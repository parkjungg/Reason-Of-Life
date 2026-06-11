using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [Header("행복도")]
    [SerializeField] private Slider happinessSlider;
    [SerializeField] private TextMeshProUGUI happinessText;

    [Header("행동력")]
    [SerializeField] private Slider apSlider;
    [SerializeField] private TextMeshProUGUI apText;

    void Start()
    {
        HappinessManager.instance.OnValueChanged += UpdateHappiness;
        ActionPointManager.instance.OnAPChanged += UpdateAP;

        UpdateHappiness(HappinessManager.instance.happiness);
        UpdateAP(ActionPointManager.instance.CurrentAP);
    }

    void OnDestroy()
    {
        HappinessManager.instance.OnValueChanged -= UpdateHappiness;
        ActionPointManager.instance.OnAPChanged -= UpdateAP;
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
}
