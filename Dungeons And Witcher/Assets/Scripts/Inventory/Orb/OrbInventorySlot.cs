using System;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class OrbInventorySlot : MonoBehaviour, IDropHandler
{
    public static event Action OnSlotChanged;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject orbDropped = eventData.pointerDrag;
        OrbItem orbItemDrag = orbDropped.GetComponent<OrbItem>();
        OrbItem currentOrbItem = GetComponentInChildren<OrbItem>();
        if (currentOrbItem != null)
        {
            currentOrbItem.SetParentAfterDrag(orbItemDrag.parentAfterDrag);
            currentOrbItem.SetTransform();
        }
        orbItemDrag.SetParentAfterDrag(transform);
        orbItemDrag.SetTransform();
        OnSlotChanged?.Invoke();
    }
    public void RemoveFromInventory()
    {
        Transform dropPosition = GameObject.Find("Player").transform;
        transform.GetChild(0).position = dropPosition.position;
    }

}
