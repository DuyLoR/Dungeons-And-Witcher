using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryBtn;
    [SerializeField] private GameObject settingBtn;

    void Update()
    {
        inventoryBtn.SetActive(!PlayerInputHandle.Instance.inventoryInput);
        settingBtn.SetActive(!PlayerInputHandle.Instance.escapeInput);
    }
}
