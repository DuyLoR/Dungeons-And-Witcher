using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour
{
    public static WeaponInfo instance;
    [Header("Image")]
    [SerializeField] private Image image;
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private TextMeshProUGUI maxManaTMP;
    [SerializeField] private TextMeshProUGUI manaRegenTMP;
    [SerializeField] private TextMeshProUGUI castDelayTMP;
    [SerializeField] private TextMeshProUGUI rechargeTimeTMP;
    [SerializeField] private TextMeshProUGUI capacityTMP;

    [Header("Orb")]
    [SerializeField] private Transform orbsPanel;
    [SerializeField] private GameObject orbSlotPrefab;
    [SerializeField] private GameObject orbItemPrefab;

    [SerializeField] private List<OrbInventorySlot> orbInventorySlots = new List<OrbInventorySlot>();
    [SerializeField] private List<OrbItem> orbItems = new List<OrbItem>();

    [Header("Parent")]
    [SerializeField] private Transform weaponInfoPanel;

    private WeaponData weaponData;

    private void Awake()
    {
        instance = this;
    }
    public void SetWeaponData(WeaponData weaponData)
    {
        this.weaponData = weaponData;
        if (weaponData == null)
        {
            return;
        }
        else
        {
            InitializeWeaponInfo();
        }
    }


    private void InitializeWeaponInfo()
    {
        image.sprite = weaponData.weaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
        nameTMP.text = weaponData.WeaponName.ToString();
        maxManaTMP.text = weaponData.maxMana.ToString();
        manaRegenTMP.text = weaponData.manaRegen.ToString();
        castDelayTMP.text = weaponData.castDelay.ToString();
        rechargeTimeTMP.text = weaponData.rechargeTime.ToString();
        capacityTMP.text = weaponData.capacity.ToString();

        GenerateOrbSlots();
        GenerateOrbItems();

    }
    private void GenerateOrbItems()
    {
        if (weaponData.orbDatas.Length == 0) return;
        for (int i = 0; i < weaponData.orbDatas.Length; i++)
        {
            if (orbItems.Count <= i)
            {
                OrbItem orbItemInSlot = orbInventorySlots[i].GetComponentInChildren<OrbItem>();
                if (orbItemInSlot == null)
                {
                    orbItems.Add(Instantiate(orbItemPrefab, orbInventorySlots[i].transform).GetComponent<OrbItem>());
                    orbItems[i].InitializeOrbItem(weaponData.orbDatas[i]);
                }
                else
                {
                    orbItems[i].InitializeOrbItem(weaponData.orbDatas[i]);
                }
            }
            else
            {
                orbItems[i].InitializeOrbItem(weaponData.orbDatas[i]);
                orbItems[i].gameObject.SetActive(true);
            }
        }
        if (orbItems.Count > weaponData.capacity)
        {
            for (int i = weaponData.capacity; i < orbItems.Count; i++)
            {
                orbItems[i].gameObject.SetActive(false);
            }
        }
    }

    private void GenerateOrbSlots()
    {
        for (int i = 0; i < weaponData.capacity; i++)
        {
            if (orbInventorySlots.Count <= i)
            {
                orbInventorySlots.Add(Instantiate(orbSlotPrefab, orbsPanel).GetComponent<OrbInventorySlot>());
            }
            else
            {
                orbInventorySlots[i].gameObject.SetActive(true);
            }
        }
        if (orbInventorySlots.Count > weaponData.capacity)
        {
            for (int i = weaponData.capacity; i < orbInventorySlots.Count; i++)
            {
                orbInventorySlots[i].gameObject.SetActive(false);
            }
        }
    }

    public bool isWeaponDataNotNull()
    {
        return weaponData != null;
    }
}
