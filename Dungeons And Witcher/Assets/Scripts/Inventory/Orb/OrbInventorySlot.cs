using System;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class OrbInventorySlot : MonoBehaviour, IDropHandler
{
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
    }
    public void RemoveItemFromInventory(OrbData orbData)
    {
        if (orbData == null) return;
        if (transform.childCount <= 0) return;
        var currentOrb = transform.GetChild(0);
        if (currentOrb != null)
        {
            var newItem = Instantiate(orbData.orbPrefab);
            newItem.transform.position = GameObject.FindWithTag("Player").transform.position;
            Destroy(currentOrb.gameObject);
        }
    }

}
