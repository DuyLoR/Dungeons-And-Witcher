using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrbItem : MonoBehaviour, ICollectible, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public static event Action<OrbData> OnOrbCollected;
    public static event Action OnOrbDroppedOnMap;
    public OrbData orbData;
    public Transform parentAfterDrag { get; private set; }
    public Transform droppedTransform { get; private set; }

    private Image image;
    private bool isRightMouseClick;
    public void InitializeOrbItem(OrbData neworbData)
    {
        this.orbData = neworbData;
        image = GetComponent<Image>();
        image.sprite = orbData.orbPrefab.GetComponent<SpriteRenderer>().sprite;
        image.preserveAspect = true;
    }
    private void Update()
    {
        isRightMouseClick = PlayerInputHandle.Instance.rightMouse;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        droppedTransform = Player.Instance.transform;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (droppedTransform == Player.Instance.transform)
        {
            var orb = Instantiate(orbData.orbPrefab);
            orb.GetComponent<Orb>().enabled = false;
            orb.GetComponent<SpriteRenderer>().flipY = false;
            orb.transform.position = droppedTransform.position;
            OnOrbDroppedOnMap?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            transform.SetParent(droppedTransform);
            image.raycastTarget = true;
        }
    }
    public void SetDroppedTransform(Transform transform)
    {
        droppedTransform = transform;
    }
    public void SetTransform()
    {
        transform.SetParent(droppedTransform);
    }

    public void Collect()
    {
        Destroy(gameObject);
        OnOrbCollected?.Invoke(orbData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTip.instance.gameObject.SetActive(true);
        ToolTip.instance.GenerateToolTip(GetComponentInChildren<OrbItem>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.instance.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isRightMouseClick)
        {
            OrbInventorySlot orbInventorySlot = gameObject.transform.parent.GetComponent<OrbInventorySlot>();
            GameObject orbSlotParent = orbInventorySlot.transform.parent.gameObject;
            if (orbSlotParent.name == "OrbInventoryPanel")
            {
                if (WeaponInfo.instance.GetEntryOrbSlot() != null)
                {
                    SetDroppedTransform(WeaponInfo.instance.GetEntryOrbSlot());
                    SetTransform();
                    WeaponInfo.instance.UpdateOrbsWeaponData();
                }
            }
            else if (orbSlotParent.name == "OrbsPanel")
            {
                if (InventoryManager.Instance.GetEntryOrbSlot() != null)
                {
                    SetDroppedTransform(InventoryManager.Instance.GetEntryOrbSlot());
                    SetTransform();
                    WeaponInfo.instance.UpdateOrbsWeaponData();
                }
            }

        }
    }
    private void OnDestroy()
    {
        if (ToolTip.instance.isActiveAndEnabled && ToolTip.instance.currentOrbItem == this)
        {
            ToolTip.instance.gameObject.SetActive(false);
        }
    }
}
