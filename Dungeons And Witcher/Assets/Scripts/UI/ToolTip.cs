using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public static ToolTip instance;
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private TextMeshProUGUI typeTMP;
    [SerializeField] private TextMeshProUGUI manaUseTMP;
    [SerializeField] private TextMeshProUGUI damageTMP;
    [SerializeField] private TextMeshProUGUI orbSpeedTMP;
    [SerializeField] private TextMeshProUGUI castDelayTMP;
    [SerializeField] private TextMeshProUGUI timeActiveTMP;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        transform.position = Input.mousePosition;
    }
    public void GenerateToolTip(OrbItem orbItem)
    {
        nameTMP.text = orbItem.orbData.orbName.ToString();
        typeTMP.text = orbItem.orbData.type.ToString();
        manaUseTMP.text = orbItem.orbData.manaUse.ToString();
        damageTMP.text = orbItem.orbData.damage.ToString();
        orbSpeedTMP.text = orbItem.orbData.orbSpeed.ToString();
        castDelayTMP.text = orbItem.orbData.castDelay.ToString();
        timeActiveTMP.text = orbItem.orbData.timeActive.ToString();
    }
}
