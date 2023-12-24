using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class WeaponInventorySlot : MonoBehaviour, IDropHandler
{
    public static event Action OnSlotChanged;
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
        OnSlotChanged?.Invoke();
    }

    public void RemoveItemFromInventory()
    {
        Transform dropPosition = GameObject.Find("Player").transform;
        transform.GetChild(0).position = dropPosition.position;
    }
}
