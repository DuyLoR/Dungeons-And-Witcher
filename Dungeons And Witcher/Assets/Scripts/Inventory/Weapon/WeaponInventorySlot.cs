using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class WeaponInventorySlot : MonoBehaviour, IDropHandler
{
    public static event Action<GameObject, int> OnSlotChanged;
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
        WeaponItem weaponItemMoveIn = weaponDropped.GetComponent<WeaponItem>();
        WeaponItem currentWeaponItem = GetComponentInChildren<WeaponItem>();
        if (currentWeaponItem != null)
        {
            currentWeaponItem.SetParentAfterDrag(weaponItemMoveIn.parentAfterDrag);
            currentWeaponItem.SetTransform();
        }
        weaponItemMoveIn.SetParentAfterDrag(transform);
        OnSlotChanged?.Invoke(gameObject, InventoryManager.Instance.GetSlotIndex(this));
    }

    public void RemoveItemFromInventory()
    {
        Destroy(transform.GetChild(0));
    }
}
