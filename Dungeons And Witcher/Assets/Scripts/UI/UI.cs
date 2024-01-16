using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI instance;
    [SerializeField] private GameObject inventoryBtn;
    [SerializeField] private GameObject settingBtn;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        inventoryBtn.SetActive(!PlayerInputHandle.Instance.inventoryInput);
        settingBtn.SetActive(!PlayerInputHandle.Instance.escapeInput);
    }
}
