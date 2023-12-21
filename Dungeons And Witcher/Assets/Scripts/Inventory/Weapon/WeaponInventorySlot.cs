using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class WeaponInventorySlot : MonoBehaviour, IDropHandler
{
    public Color selectedColor, notSelectedColor;

    private Image image;

    public bool isFull { get; private set; }
    private void Awake()
    {
        image = GetComponent<Image>();
        Deselect();
    }
    public void Select()
    {
        image.color = selectedColor;
    }
    public void Deselect()
    {
        image.color = notSelectedColor;
    }
    public WeaponInventorySlot(WeaponData weaponData)
    {
        AddToInventory();
    }
    public void AddToInventory()
    {
        isFull = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject weaponDropped = eventData.pointerDrag;
        WeaponItem weaponItem = weaponDropped.GetComponent<WeaponItem>();
        weaponItem.SetParentAfterDrag(transform);
    }

    public void RemoveFromInventory()
    {
        isFull = false;
    }
}
