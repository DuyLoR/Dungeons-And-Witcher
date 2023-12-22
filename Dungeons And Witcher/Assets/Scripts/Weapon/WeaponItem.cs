using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public WeaponData weaponData { get; private set; }
    public Transform parentAfterDrag { get; private set; }

    private Image image;
    public void InitializeWeaponItem(WeaponData newWeapodata)
    {
        this.weaponData = newWeapodata;
        image = GetComponent<Image>();
        image.sprite = weaponData.weaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
    }
    public void InitalizeWeaponData(WeaponData newWeaponData)
    {
        this.weaponData = newWeaponData;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;

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
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void SetParentAfterDrag(Transform transform)
    {
        parentAfterDrag = transform;
    }
    public void SetTransform()
    {
        transform.SetParent(parentAfterDrag);
    }
}
