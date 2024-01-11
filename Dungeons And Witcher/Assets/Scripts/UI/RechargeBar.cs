using UnityEngine;
using UnityEngine.UI;

public class RechargeBar : MonoBehaviour
{
    public static RechargeBar instance;
    public Slider slider { get; private set; }

    private float timer;
    private bool isStartRechargeTime;
    private void Awake()
    {
        instance = this;
        slider = GetComponent<Slider>();
        gameObject.SetActive(false);
        timer = Time.time;
    }
    public void SetMaxRechargeTime(float time)
    {
        slider.maxValue = time;
        slider.value = 0;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isStartRechargeTime)
        {
            slider.value += Time.deltaTime;
            if (Time.time >= timer + slider.maxValue)
            {
                slider.value = 0;
                isStartRechargeTime = false;
                gameObject.SetActive(false);
            }
        }
    }
    public void StartRechargeTime(float time)
    {
        slider.maxValue = time;
        gameObject.SetActive(true);
        timer = Time.time;
        isStartRechargeTime = true;
    }
    public void StopRechargeTime()
    {
        gameObject.SetActive(false);
    }
}
