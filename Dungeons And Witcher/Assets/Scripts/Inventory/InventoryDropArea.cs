using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDropArea : MonoBehaviour, IDropHandler
{
    private Transform playerPos;
    private void Awake()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject ItemDropped = eventData.pointerDrag;
        WeaponItem weaponItemDrag = ItemDropped.GetComponent<WeaponItem>();
        if (weaponItemDrag != null)
        {
            InventoryManager.Instance.RemoveWeapon(weaponItemDrag.parentAfterDrag, weaponItemDrag.weaponData);
            var DroppedItem = Instantiate(weaponItemDrag.weaponData.weaponPrefab);
            DroppedItem.transform.position = playerPos.position;
        }
        OrbItem orbItemDrag = ItemDropped.GetComponent<OrbItem>();
        if (orbItemDrag != null)
        {
            InventoryManager.Instance.RemoveOrb(orbItemDrag.parentAfterDrag, orbItemDrag.orbData);
            var DroppedItem = Instantiate(orbItemDrag.orbData.orbPrefab);
            DroppedItem.transform.position = playerPos.position;
        }
    }
}
