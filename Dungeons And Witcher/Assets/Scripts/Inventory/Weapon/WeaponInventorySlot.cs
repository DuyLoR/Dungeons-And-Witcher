using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class WeaponInventorySlot : MonoBehaviour, IDropHandler
{
    public Color selectedColor, notSelectedColor;

    private void Awake()
    {
        Deselect();
    }
    public void Select()
    {
        GetComponent<Image>().color = selectedColor;
    }
    public void Deselect()
    {
        GetComponent<Image>().color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject weaponDropped = eventData.pointerDrag;
        WeaponItem weaponItemDrag = weaponDropped.GetComponent<WeaponItem>();
        WeaponItem currentWeaponItem = GetComponentInChildren<WeaponItem>();
        if (currentWeaponItem != null)
        {
            currentWeaponItem.SetParentAfterDrag(weaponItemDrag.parentAfterDrag);
            currentWeaponItem.SetTransform();
        }
        weaponItemDrag.SetParentAfterDrag(transform);
        weaponItemDrag.SetTransform();
    }

    public void RemoveItemFromInventory(WeaponData weaponData)
    {
        if (weaponData == null) return;
        if (transform.childCount <= 0) return;
        var currentWeapon = transform.GetChild(0);
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
            var newItem = Instantiate(weaponData.weaponPrefab);
            newItem.transform.position = GameObject.FindWithTag("Player").transform.position;
        }
    }
}
