using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealBar : MonoBehaviour
{
    public static HealBar instance;
    public Slider slider;
    public TextMeshProUGUI text { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        text.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        text.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
    }
}
