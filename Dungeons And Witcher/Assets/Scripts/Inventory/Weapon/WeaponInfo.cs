using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField]
    private Image spriteImage;

    [SerializeField]
    private GameObject statsPanel;
    [SerializeField]
    private GameObject OrbsPanel;

    [SerializeField]
    private GameObject statPrefab;
    [SerializeField]
    private GameObject OrbInventoryPrefab;

    [SerializeField]
    private List<OrbInventorySlot> orbInventorySlots = new List<OrbInventorySlot>();
    private void OnEnable()
    {
        InventoryManager.OnWeaponData += InventoryManager_OnWeaponData;
    }
    private void OnDisable()
    {
        InventoryManager.OnWeaponData -= InventoryManager_OnWeaponData;
    }
    private void InventoryManager_OnWeaponData(WeaponData weaponData)
    {
        if (weaponData.orbDatas != null)
        {
            for (int i = 0; i < weaponData.orbDatas.Length; i++)

            {
                orbInventorySlots.Add(Instantiate(OrbInventoryPrefab).GetComponent<OrbInventorySlot>());
                orbInventorySlots[i].transform.SetParent(OrbsPanel.transform);
            }
            for (int i = 0; i < orbInventorySlots.Count; i++)
            {
                var orb = Instantiate(weaponData.orbDatas[i].orbPrefab);
                orb.transform.SetParent(orbInventorySlots[i].transform);

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
