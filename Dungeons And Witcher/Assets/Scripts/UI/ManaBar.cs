using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public static ManaBar instance;
    public Slider slider { get; private set; }
    public TextMeshProUGUI text { get; private set; }

    private void Awake()
    {
        instance = this;
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        text.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
    }
    public void SetMaxMana(int mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }
    public void SetMana(int mana)
    {
        slider.value = mana;
        text.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
    }
}
