using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicUI : MonoBehaviour
{
    [SerializeField]
    private Button relicBtnPrefab;
    [SerializeField]
    private int maxRelicInUI = 2;
    [SerializeField]
    public List<RelicData> relicDatas;
    private List<Button> relicBtns = new List<Button>();
    private void Start()
    {
        Time.timeScale = 0f;
        for (int i = 0; i < maxRelicInUI; i++)
        {
            Button newButton = Instantiate(relicBtnPrefab, transform);
            relicBtns.Add(newButton);
            TextMeshProUGUI[] buttonTexts = newButton.GetComponentsInChildren<TextMeshProUGUI>();
            if (buttonTexts != null)
            {
                int index = Random.Range(0, relicDatas.Count - 1);
                buttonTexts[0].text = relicDatas[index].Name + ":";
                buttonTexts[1].text = "+" + relicDatas[index].value.ToString();
                newButton.onClick.AddListener(() => OnBtnClick(relicDatas[index]));
            }
        }
    }
    private void OnBtnClick(RelicData data)
    {
        if (data.buffType == RelicData.Buff.heal)
        {
            HealBar.instance.SetMaxHealth(data.value + HealBar.instance.GetMaxHealth());
            Player.Instance.UpCurrentHeal(data.value);
        }
        if (data.buffType == RelicData.Buff.movementSpeed)
        {
            Player.Instance.upMomentSpeed += data.value;
        }
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
    public void UpdateBuff()
    {
    }
}
