using System;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class OrbInventorySlot : MonoBehaviour, IDropHandler
{
    public OrbData orbData { get; private set; }
    public bool isFull { get; private set; }
    public OrbInventorySlot(OrbData orbData)
    {
        this.orbData = orbData;
        AddToInventory();
    }

    public void AddToInventory()
    {
        isFull = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject orbDropped = eventData.pointerDrag;
        OrbItem orbItem = orbDropped.GetComponent<OrbItem>();
        orbItem.SetParentAfterDrag(transform);
    }
    public void RemoveFromInventory()
    {
        isFull = false;
    }

}
