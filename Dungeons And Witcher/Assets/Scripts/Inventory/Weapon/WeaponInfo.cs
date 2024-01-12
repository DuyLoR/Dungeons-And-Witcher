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
    [SerializeField] private TextMeshProUGUI capacity;

    [Header("Orb")]
    [SerializeField] public Transform orbsPanel;
    [SerializeField] private GameObject orbSlotPrefab;
    [SerializeField] private GameObject orbItemPrefab;

    [SerializeField] private List<OrbInventorySlot> orbInventorySlots = new List<OrbInventorySlot>();
    [SerializeField] public List<OrbItem> orbItems = new List<OrbItem>();

    [Header("Parent")]
    [SerializeField] private Transform weaponInfoPanel;

    private WeaponData weaponData;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        OrbInventorySlot.OnOrbsUpdated += UpdateOrbsWeaponData;
        OrbItem.OnOrbDroppedOnMap += UpdateOrbsWeaponData;
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
        castDelayTMP.text = (weaponData.orbDatas.Length - 1).ToString();

        GenerateOrbSlots();
        GenerateOrbItems();

    }
    private void GenerateOrbItems()
    {
        ClearOrbItems();
        if (weaponData.orbDatas.Length == 0) return;
        for (int i = 0; i < weaponData.orbDatas.Length; i++)
        {
            if (orbItems.Count <= i)
            {
                OrbItem orbItemInSlot = orbInventorySlots[i].GetComponentInChildren<OrbItem>();
                if (orbItemInSlot == null)
                {
                    if (weaponData.orbDatas[i] == null)
                    {
                        orbItems.Add(null);
                        continue;
                    }
                    orbItems.Add(Instantiate(orbItemPrefab, orbInventorySlots[i].transform).GetComponent<OrbItem>());
                    orbItems[i].InitializeOrbItem(weaponData.orbDatas[i]);
                }
                else
                {
                    if (weaponData.orbDatas[i] == null)
                    {
                        continue;
                    }
                    orbItems[i] = Instantiate(orbItemPrefab, orbInventorySlots[i].transform).GetComponent<OrbItem>();
                    orbItems[i].InitializeOrbItem(weaponData.orbDatas[i]);
                }
            }
            else
            {
                if (weaponData.orbDatas[i] == null) continue;
                orbItems[i] = Instantiate(orbItemPrefab, orbInventorySlots[i].transform).GetComponent<OrbItem>();
                orbItems[i].InitializeOrbItem(weaponData.orbDatas[i]);
            }
        }
    }

    private void ClearOrbItems()
    {
        if (orbItems.Count == 0) return;
        for (int i = 0; i < orbItems.Count; i++)
        {
            if (orbItems[i] == null) continue;
            Destroy(orbItems[i].gameObject);
            orbItems[i] = null;
        }
    }

    private void GenerateOrbSlots()
    {
        for (int i = 0; i < weaponData.orbDatas.Length; i++)
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
        if (orbInventorySlots.Count > weaponData.orbDatas.Length)
        {
            for (int i = weaponData.orbDatas.Length; i < orbInventorySlots.Count; i++)
            {
                orbInventorySlots[i].gameObject.SetActive(false);
            }
        }
    }
    public void UpdateOrbsWeaponData()
    {
        if (weaponData == null) return;
        if (weaponData.orbDatas.Length == 0) return;
        for (int i = 0; i < orbsPanel.childCount; i++)
        {
            var orbItem = orbsPanel.GetChild(i).GetComponentInChildren<OrbItem>();
            orbItems[i] = orbItem;
        }

        for (int i = 0; i < weaponData.orbDatas.Length; i++)
        {
            if (orbItems[i] == null)
            {
                weaponData.orbDatas[i] = null;
                continue;
            }
            weaponData.orbDatas[i] = orbItems[i].GetComponent<OrbItem>().orbData;
        }
    }

    public int GetFirstOrb()
    {
        if (weaponData == null) return -1;
        for (int i = 0; i < weaponData.orbDatas.Length; i++)
        {
            if (weaponData.orbDatas[i] != null)
            {
                return i;
            }
        }
        return -1;
    }
    public Transform GetEntryOrbSlot()
    {
        for (int i = 0; i < orbItems.Count; i++)
        {
            if (orbItems[i] == null)
            {
                return orbsPanel.GetChild(i).transform;
            }
        }
        return null;
    }
    public bool isWeaponDataNotNull()
    {
        return weaponData != null;
    }
}
