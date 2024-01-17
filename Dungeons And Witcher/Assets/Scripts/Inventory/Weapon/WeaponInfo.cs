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
        for (int i = 0; i < orbItems.Count; i++)
        {
            Debug.Log(orbItems[i]);
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
        capacity.text = (weaponData.orbDatas.Length).ToString();

        GenerateOrbSlots();
        GenerateOrbItems();

    }
    private void GenerateOrbItems()
    {
        ClearOrbItems();
        int orbDataLength = weaponData.orbDatas.Length;
        for (int i = 0; i < orbInventorySlots.Count; i++)
        {
            orbItems.Add(null);
            if (i >= orbDataLength)
            {
                continue;
            }
            if (weaponData.orbDatas[i] != null)
            {
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
        }
        orbItems.Clear();
    }

    private void GenerateOrbSlots()
    {
        ClearOrbSlots();
        for (int i = 0; i < weaponData.orbDatas.Length; i++)
        {
            orbInventorySlots.Add(Instantiate(orbSlotPrefab, orbsPanel).GetComponent<OrbInventorySlot>());
        }
    }
    private void ClearOrbSlots()
    {
        if (orbInventorySlots.Count == 0) return;
        for (int i = 0; i < orbInventorySlots.Count; i++)
        {
            if (orbInventorySlots[i] == null) continue;
            Destroy(orbInventorySlots[i].gameObject);
        }
        orbInventorySlots.Clear();
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
